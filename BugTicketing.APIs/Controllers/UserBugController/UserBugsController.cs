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
        [HttpPost]
        [Route("{id}")]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>, NotFound<GeneralResult>>> Add(string id,UserBugAddDto userBugAddDto)
        {
            var result = await userBugMangerRepo.AddAsync(id,userBugAddDto);
            if (result.Status)
            {
                return TypedResults.Ok(result);
            }
            if (result.Status && result.Errors[0].Code == "404")
            {
                return TypedResults.NotFound(result);
            }
            return TypedResults.BadRequest(result);
        }
        
        [HttpDelete]
        [Route("{bugId}/{userId}")]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>, NotFound<GeneralResult>>> Delete(string bugId,string userId)
        {
            var result = await userBugMangerRepo.DeleteAsync(bugId,userId);
            if (result.Status)
            {
                return TypedResults.Ok(result);
            }
            if (result.Status && result.Errors[0].Code == "404")
            {
                return TypedResults.NotFound(result);
            }
            return TypedResults.BadRequest(result);
        }


    }
}
