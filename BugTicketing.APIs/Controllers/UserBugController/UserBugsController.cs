using BugTicketing.BL.Commen;
using BugTicketing.BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BugTicketing.APIs.Controllers.UserBugController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserBugsController : ControllerBase
    {
        private readonly IUserBugMangerRepo userBugMangerRepo;

        public UserBugsController(IUserBugMangerRepo userBugMangerRepo)
        {
            this.userBugMangerRepo = userBugMangerRepo;
        }
        


    }
}
