using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BugTicketing.DAL
{
    public static class ExtentionsDAL
    {
        public static void AddExtentionsDAL(this IServiceCollection services ,IConfiguration configuration )
        {

            #region Database

            var connectionString = configuration
                .GetConnectionString("default");

            Console.WriteLine(connectionString);

           services.AddDbContext<Context>(options =>
                options.UseSqlServer(connectionString));

            #endregion

            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IProjectRepo, ProjrctRepo>();
            services.AddScoped<IBugRepo, BugRepo>();
            services.AddScoped<IUserBugRepo, UserBugRepo>();
            services.AddScoped<IAttachmentRepo , AttachmentRepo>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

        }
    }
}
