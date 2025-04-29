using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BugTicketing.DAL
{
    public class Context: IdentityDbContext<User>
    {
        public Context(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);
        }

        #region Tabls

        public DbSet<User> users => Set<User>();
        public DbSet<Project> projects => Set<Project>();
        public DbSet<Bug> Bugs => Set<Bug>();
        public DbSet<Attachment> attachments => Set<Attachment>();
        public DbSet<UserBug> UserBugs => Set<UserBug>();

        #endregion
    }
}
