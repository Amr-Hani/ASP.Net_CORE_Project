namespace BugTicketing.DAL
{
    public interface IUnitOfWork
    {
        IUserRepo UserRepo { get; }
        IProjectRepo ProjectRepo { get; }
        IBugRepo BugRepo { get; }
        IUserBugRepo UserBugRepo { get; }
        IAttachmentRepo AttachmentRepo{ get; }


        Task<int> SaveChangesAsync();
    }
}
