
using Microsoft.EntityFrameworkCore;

namespace BugTicketing.DAL
{
    public class UserBugRepo : IUserBugRepo
    {
        private readonly Context context;

        public UserBugRepo(Context context)
        {
            this.context = context;
        }
        public void Add(UserBug userBug)
        {
            context.Set<UserBug>().Add(userBug);
        }

        public async Task<UserBug> GetUserBugByIdAsync(string userId, string BugId)
        {
           return await context.Set<UserBug>().FirstOrDefaultAsync(ub=>ub.User_Id == userId && ub.Bug_Id == BugId);
        }

        public void Remove(UserBug userBug)
        {
            context.Set<UserBug>().Remove(userBug);
        }
    }
}
