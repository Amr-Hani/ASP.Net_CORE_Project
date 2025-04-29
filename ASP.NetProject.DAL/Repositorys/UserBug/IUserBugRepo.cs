namespace BugTicketing.DAL
{
    public interface IUserBugRepo
    {
       
        public void Add(UserBug userBug);
        
        public void Remove(UserBug userBug);
        public Task<UserBug> GetUserBugByIdAsync (string userId , string BugId);

       
    }
}
