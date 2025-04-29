namespace BugTicketing.DAL
{
    public interface IBugRepo:IGenericRepo<Bug>
    {
        public Task<Bug> GetBugByIdWithDetailsAsync(string id);
        public Task<Bug> GetByIdAsync(string id);

    }
}
