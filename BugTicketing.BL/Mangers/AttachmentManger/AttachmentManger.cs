using BugTicketing.BL.Commen;
using BugTicketing.DAL;

namespace BugTicketing.BL
{
    public class AttachmentManger : IAttachmentManger
    {
        private readonly IUnitOfWork unitOfWork;

        public AttachmentManger(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<GeneralResult> AddAttachementAsync(string bug_id, FileUploadRequest fileRequest)
        {
            bool bug = await IsBugIdFound(bug_id);
            if (!bug)
            {
                return new GeneralResult
                {
                    Status = false,
                    Errors = ErrorResults("400", "no bug with this id")
                };

            }
            var file = fileRequest.File;
            #region Validation

            if (file.Length == 0)
            {
                return new GeneralResult
                {
                    Status = false,
                    Errors = ErrorResults("400", "you must uplad file")
                };
            }
            if (file.Length > 5 * 1024 * 1024)
            {

                return new GeneralResult
                {
                    Status = false,
                    Errors = ErrorResults("400", "File is too large")
                };

            }
            var exteenstion = Path.GetExtension(file.FileName).ToLowerInvariant();
            #endregion
            var filePath = Path.Combine(
               Directory.GetCurrentDirectory(),
               "Images", $"{Guid.NewGuid()}{exteenstion}"
               );
            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);
            string fileURL = $"/api/my-static-files/{Path.GetFileName(filePath)}";

            Attachment attachementToBeAdded = new Attachment
            {
                Attachment_Name = file.FileName,
                Attachment_URL = fileURL,
                Bug_Id = bug_id
            };

            unitOfWork.AttachmentRepo.Add(attachementToBeAdded);
            var saveResult = await unitOfWork.SaveChangesAsync();

            if (saveResult > 0)
            {
                return new GeneralResult
                {
                    Status = true
                };
            }
            else
            {

                return new GeneralResult
                {
                    Status = false,
                    Errors = ErrorResults("400", "unable to add this Attachement")
                };


            }




        }

        public async Task<GeneralResult<ShowAllAttachmentWithBug>> GetBugByIdWithDetailsAsync(string id)
        {
            var bug = await unitOfWork.BugRepo.GetBugByIdWithDetailsAsync(id);
            return new GeneralResult<ShowAllAttachmentWithBug>
            {
                Status = true,
                Data = new ShowAllAttachmentWithBug
                {
                    Bug_Description = bug.Bug_Description,
                    Status = bug.Status,
                    Bug_Id = bug.Bug_Id,
                    Bug_Name = bug.Bug_Name,
                    Bug_Type = bug.Bug_Type,
                    CreatedAt = bug.CreatedAt,
                    priority = bug.priority,
                    AttachmentBugs = bug.Attachments.Select(ba => new AttachmentBugg
                    {
                        Attachment_Id = ba.Attachment_id,
                        Attachment_Name = ba.Attachment_Name,
                        Attachment_URL = ba.Attachment_URL,
                    }).ToList()
                }
            };
        }
        private async Task<bool> IsBugIdFound(string bug_id)
        {
            return await unitOfWork.BugRepo.GetByIdAsync(bug_id) != null;
        }

        private ResultError[] ErrorResults(string code, string message)
        {
            return 
                new ResultError[]
            {
                new ResultError
                {
                    Code = code,
                    Message =message
                }
            };
        }

        public async Task<GeneralResult> DeleteAttachmentFromBug(Guid attachmentId)
        {
            var attachment = await unitOfWork.AttachmentRepo.GetByIdAsync(attachmentId);
            if (attachment == null)
            {
                return new GeneralResult
                {
                    Status = false,
                    Errors = new ResultError[]
                        {
                            new ResultError
                            {
                                Code = "404",
                                Message = "attachment Not Found"
                            }
                    }
                };
            }

            unitOfWork.AttachmentRepo.Delete(attachment);
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0)
            {
                return new GeneralResult
                {
                    Status = true
                };
            }
            return new GeneralResult
            {
                Status = true
            };
        }
    }
}
