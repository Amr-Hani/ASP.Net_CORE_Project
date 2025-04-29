using System.Text;
using BugTicketing.BL.Mangers.BugManger;
using BugTicketing.DAL;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;


namespace BugTicketing.BL
{
    public static class ExtentionBL
    {
        public static void AddExtentionBL(this IServiceCollection services)
        {
            services.AddScoped<IUserMangerRepo, UserMangerRepo>();
            services.AddScoped<IProjectManger, ProjectManger>();
            services.AddScoped<IBugManger, BugManger>();
            services.AddScoped<IUserBugMangerRepo, UserBugMangerRepo>();
            services.AddScoped<IAttachmentManger, AttachmentManger>();
            //services.AddScoped<IValidator<Register>, UserRegisterDTOValidator>();
            //services.AddScoped<IValidator<ProjectAddDto>, ProjectAddDtoValidator>();
            services.AddValidatorsFromAssembly(typeof(ExtentionBL).Assembly);

            services.AddIdentityCore<User>(options =>
            {
                // Validation to be read from configurations
                options.Password.RequiredUniqueChars = 2;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;

                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<Context>();


           
        }
    }
}
