using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BugTicketing.BL;
using BugTicketing.BL.Commen;
using BugTicketing.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;

namespace BugTicketing.APIs.Controllers.UserController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration configuration;

        private readonly IUserMangerRepo userMangerRepo;
        private readonly UserManager<User> userManager;

        public UsersController(IUserMangerRepo userMangerRepo, IConfiguration configuration,
        UserManager<User> userManager)
        {
            this.configuration = configuration;
            this.userMangerRepo = userMangerRepo;
            this.userManager = userManager;

        }
        //[HttpGet]
        //public async Task<Ok<Register>> GetAll()
        //{

        //    return TypedResults.Ok();
        //}

        [HttpPost]
        [Route("register")]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> Add(Register register)
        {
            var result = await userMangerRepo.Regisetr(register);
            if (result.Status)
            {
                return TypedResults.Ok(result);
            }
            return TypedResults.BadRequest(result);
        }

       

        [HttpPost]
        [Route("login")]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>>
      //public async Task<IActionResult> Login(Login login)
      Login(Login login)
        {
            var result = await userMangerRepo.Login(login);

            if (result.Status)
            {
                return TypedResults.Ok(result);
            }
            else
            {
                return TypedResults.BadRequest(result);
            }
        }
    }
}
