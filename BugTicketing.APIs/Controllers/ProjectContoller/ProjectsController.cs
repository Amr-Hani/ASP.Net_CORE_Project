using BugTicketing.BL.Commen;
using BugTicketing.BL;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace BugTicketing.APIs.Controllers.ProjectContoller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectManger projectManger;

        public ProjectsController(IProjectManger projectManger)
        {
            this.projectManger = projectManger;
        }
        [HttpPost]
        [Authorize]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> Add(ProjectAddDto projectAddDto)
        {
            var result = await projectManger.Add(projectAddDto);
            if (result.Status)
            {
                return TypedResults.Ok(result);
            }
            return TypedResults.BadRequest(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<Ok<GeneralResult<List<ProjectShowDto>>>> GetAll()
        {
            var result = await projectManger.GetAllAsync();
            return TypedResults.Ok(result);
        }
        
        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public async Task<Ok<GeneralResult<ProjectShowWithDetailsDto>>> GetByIdWithDetailsAll(Guid id)
        {
            var result = await projectManger.GetByIdWithDetailsAsync(id);
            return TypedResults.Ok(result);
        }
    }
}
