namespace BugTicketing.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Context context;

        public IUserRepo UserRepo { get; }

        public IProjectRepo ProjectRepo { get; }

        public IBugRepo BugRepo { get; }

        public IUserBugRepo UserBugRepo { get; }

        public IAttachmentRepo AttachmentRepo { get; }

        public UnitOfWork(Context context, IUserRepo userRepo, IProjectRepo projectRepo,IBugRepo bugRepo ,IUserBugRepo userBugRepo, IAttachmentRepo attachmentRepo)
        {
            this.context = context;
            UserRepo = userRepo;
            ProjectRepo = projectRepo;
            BugRepo = bugRepo;
            UserBugRepo = userBugRepo;
            AttachmentRepo = attachmentRepo;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}
