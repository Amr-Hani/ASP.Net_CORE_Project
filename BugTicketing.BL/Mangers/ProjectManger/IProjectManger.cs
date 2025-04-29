using BugTicketing.BL.Commen;

namespace BugTicketing.BL
{
    public interface IProjectManger
    {
        public Task<GeneralResult> Add(ProjectAddDto projectAddDto);
        public Task<GeneralResult<List<ProjectShowDto>>> GetAllAsync(); 
        public Task<GeneralResult> GetByNameAsync(string name);

        public Task<GeneralResult<ProjectShowWithDetailsDto>> GetByIdWithDetailsAsync(Guid id);

    }
}
