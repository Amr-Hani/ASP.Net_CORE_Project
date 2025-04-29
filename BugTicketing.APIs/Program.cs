using System.Text;
using BugTicketing.BL;
using BugTicketing.DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();


        #region DALExtention

        builder.Services.AddExtentionsDAL(builder.Configuration);

        #endregion

        #region BLExtention

        builder.Services.AddExtentionBL();


        #endregion
        #region Authentication

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var secretKey = builder.Configuration.GetValue<string>("ScretKey")!;

                var secretKeyInBytes = Encoding.UTF8.GetBytes(secretKey);
                var key = new SymmetricSecurityKey(secretKeyInBytes);

                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = key,
                };
            });

        #endregion


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapScalarApiReference();
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();


        var imagesFolder = Path.Combine(

        Directory.GetCurrentDirectory(),
        "Images");
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(imagesFolder),
            RequestPath = "/api/my-static-files"
        });


        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}

#region DalExtention

#endregion
