using BugTicketing.BL.Commen;
using BugTicketing.BL;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BugTicketing.APIs.Controllers.BugController
{
    [Route("api/[controller]")]
    [ApiController]
    public class BugsController : ControllerBase
    {
        private readonly IBugManger bugManger;
        private readonly IAttachmentManger attachmentManger;

        public BugsController(IBugManger bugManger,IAttachmentManger attachmentManger)
        {
            this.bugManger = bugManger;
            this.attachmentManger = attachmentManger;
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


        //----------------------------------------------------add attachement to Bug ------------------------------------------------------------------------------//
        [HttpPost("{bug_Id}/attachments")]

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
        public async Task<Ok<GeneralResult<ShowAllAttachmentWithBug>>> GetAttachmentByIdWithBug(string bug_Id)
        {
            var result = await attachmentManger.GetBugByIdWithDetailsAsync(bug_Id);
            return TypedResults.Ok(result);
        }


        [HttpDelete]
        [Route("{bugId}/attachments/{attachmentId}")]
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
