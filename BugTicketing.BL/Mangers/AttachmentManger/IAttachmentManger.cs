using BugTicketing.BL.Commen;

namespace BugTicketing.BL
{
    public interface IAttachmentManger
    {
        public Task<GeneralResult> AddAttachementAsync(string bug_id, FileUploadRequest fileRequest);
        public Task<GeneralResult<ShowAllAttachmentWithBug>> GetBugByIdWithDetailsAsync(string id);
        public Task<GeneralResult> DeleteAttachmentFromBug(Guid attachmentId);
    }
}
