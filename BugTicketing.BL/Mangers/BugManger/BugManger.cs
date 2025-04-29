using BugTicketing.BL.Commen;
using BugTicketing.DAL;
using FluentValidation;

namespace BugTicketing.BL.Mangers.BugManger
{
    public class BugManger : IBugManger
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IValidator<BugAddDto> validator;

        public BugManger(IUnitOfWork unitOfWork, IValidator<BugAddDto> validator
            )
        {
            this.unitOfWork = unitOfWork;
            this.validator = validator;
        }
        public async Task<GeneralResult> Add(BugAddDto bugAddDto)
        {
            var validationResult = await validator.ValidateAsync(bugAddDto);
            if (!validationResult.IsValid)
            {
                return new GeneralResult
                {
                    Status = false,
                    Errors = validationResult.Errors.Select(e => new ResultError
                    {
                        Code = e.ErrorCode,
                        Message = e.ErrorMessage
                    }).ToArray()
                };
            }
            Bug bug = new Bug
            {
                Bug_Id = Guid.NewGuid().ToString(),
                Bug_Description = bugAddDto.Bug_Description,
                Bug_Name = bugAddDto.Bug_Name,
                Bug_Type = bugAddDto.Bug_Type,
                priority = BugPriority.MEDIUM,
                Status = BugStatus.InProcess,
                Project_Id = bugAddDto.Project_Id,
            };

            if(unitOfWork is null) Console.WriteLine("unit of work is null");
            if(unitOfWork.BugRepo is null) Console.WriteLine("unit of work is null");
            unitOfWork.BugRepo.Add(bug);
          
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0)
            {

                return new GeneralResult
                {
                    Status = true
                };
            }
            else
                return new GeneralResult
                {
                    Status = false,
                    Errors = [new ResultError{
                        Code = "400",
                        Message = "hazel error fel inser"
                    }],
                };
        }

        public async Task<GeneralResult<List<BugShowDto>>> GetAllAsync()
        {

            var bugs = await unitOfWork.BugRepo.GetAllAsync();
            var bugDto = bugs.Select(bug => new BugShowDto
            {
                Bug_Description = bug.Bug_Description,
                Bug_Name = bug.Bug_Name,
                Bug_Id = bug.Bug_Id,
                Status = bug.Status,
                priority = bug.priority
            }).ToList();

            return new GeneralResult<List<BugShowDto>>
            {
                Status = true,
                Data = bugDto
            };
        }

        public async Task<GeneralResult<ShowBugWithDetails>> GetBugByIdWithDetailsAsync(string id)
        {
            var bug = await unitOfWork.BugRepo.GetBugByIdWithDetailsAsync(id);
            return new GeneralResult<ShowBugWithDetails>
            {
                Status = true
                ,
                Data = new ShowBugWithDetails
                {
                    Bug_Description = bug.Bug_Description,
                    Status = bug.Status,
                    Bug_Id = bug.Bug_Id,
                    Bug_Name = bug.Bug_Name,
                    Bug_Type = bug.Bug_Type,
                    CreatedAt = bug.CreatedAt,
                    priority = bug.priority,
                    AttachmentBugs = bug.Attachments.Select(ba => new AttachmentBug
                    {
                        Attachment_Name = ba.Attachment_Name,
                        Attachment_Image = ba.Attachment_URL,
                    }).ToList(),
                    UserBugs = bug.Users.Select(bu => new UserBug
                    {
                        Name = bu.User.UserName
                    }).ToList()


                }
            };


        }
    }
}
