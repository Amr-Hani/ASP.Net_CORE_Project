using BugTicketing.BL.Commen;
using BugTicketing.BL;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace BugTicketing.APIs.Controllers.BugController
{
    [Route("api/[controller]")]
    [ApiController]
    public class BugsController : ControllerBase
    {
        private readonly IBugManger bugManger;
        private readonly IAttachmentManger attachmentManger;
        private readonly IUserBugMangerRepo userBugMangerRepo;

        public BugsController(IBugManger bugManger,IAttachmentManger attachmentManger,IUserBugMangerRepo userBugMangerRepo)
        {
            this.bugManger = bugManger;
            this.attachmentManger = attachmentManger;
            this.userBugMangerRepo = userBugMangerRepo;
        }
        [HttpPost]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> Add(BugAddDto bugAddDto)
        {
            var result = await bugManger.Add(bugAddDto);
            if (result.Status)
            {
                return TypedResults.Ok(result);
            }
            return TypedResults.BadRequest(result);
        }
        [HttpGet]
        public async Task<Ok<GeneralResult<List<BugShowDto>>>>GetAll()
        {
            var result = await bugManger.GetAllAsync();
            return TypedResults.Ok(result);
        }
        
        [HttpGet]
        [Route("{id}")]
        public async Task<Ok<GeneralResult<ShowBugWithDetails>>>GetBugByIdWithDetails(string id)
        {
            var result = await bugManger.GetBugByIdWithDetailsAsync(id);
            Console.WriteLine(id);
            return TypedResults.Ok(result);
        }

        //---------------------------------------------------- User_Bug ------------------------------------------------------------------------------\\
        [HttpPost]
        [Route("{id}/assignees")]
        [Authorize]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>, NotFound<GeneralResult>>> Add(string id, UserBugAddDto userBugAddDto)
        {
            var result = await userBugMangerRepo.AddAsync(id, userBugAddDto);
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
        [Route("{bugId}/assignees/{userId}")]
        [Authorize]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>, NotFound<GeneralResult>>> Delete(string bugId, string userId)
        {
            var result = await userBugMangerRepo.DeleteAsync(bugId, userId);
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



        //---------------------------------------------------- Attachement ------------------------------------------------------------------------------\\
        [HttpPost("{bug_Id}/attachments")]
        [Authorize]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> AddAsync(string bug_Id, [FromForm] FileUploadRequest fileRequest)
        {
            var result = await attachmentManger.AddAttachementAsync(bug_Id, fileRequest);

            if (result.Status)
            {
                return TypedResults.Ok(result);
            }
            return TypedResults.BadRequest(result);
        }

        [HttpGet]
        [Route("{bug_Id}/attachments")]
        [Authorize]
        public async Task<Ok<GeneralResult<ShowAllAttachmentWithBug>>> GetAttachmentByIdWithBug(string bug_Id)
        {
            var result = await attachmentManger.GetBugByIdWithDetailsAsync(bug_Id);
            return TypedResults.Ok(result);
        }


        [HttpDelete]
        [Route("{bugId}/attachments/{attachmentId}")]
        [Authorize]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>, NotFound<GeneralResult>>> Delete(string bugId, Guid attachmentId)
        {
            var result = await attachmentManger.DeleteAttachmentFromBug(attachmentId);
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
