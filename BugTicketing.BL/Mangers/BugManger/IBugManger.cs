using BugTicketing.BL.Commen;

namespace BugTicketing.BL
{
    public interface IBugManger
    {
        public Task<GeneralResult> Add(BugAddDto bugAddDto);
        public Task<GeneralResult<List<BugShowDto>>> GetAllAsync();
        public Task<GeneralResult<ShowBugWithDetails>> GetBugByIdWithDetailsAsync(string id);
    }
}
