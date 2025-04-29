using Microsoft.EntityFrameworkCore;

namespace BugTicketing.DAL
{
    public class BugRepo : GenericRepo<Bug>, IBugRepo
    {
        private readonly Context context;

        public BugRepo(Context context) : base(context)
        {
            this.context = context;
        }

        public async Task<Bug> GetByIdAsync(string id)
        {
           return await context.Set<Bug>().FirstOrDefaultAsync(b => b.Bug_Id == id);
        }
        public async Task<Bug> GetBugByIdWithDetailsAsync(string id)
        {
            return await context.Set<Bug>().Include(b => b.Attachments).Include(b => b.Users).ThenInclude(b=>b.User).FirstOrDefaultAsync(b => b.Bug_Id == id);
        }
    }
}
