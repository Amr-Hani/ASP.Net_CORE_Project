using BugTicketing.BL.Commen;
using BugTicketing.DAL;
using FluentValidation;

namespace BugTicketing.BL
{
    public class ProjectManger : IProjectManger
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IValidator<ProjectAddDto> validator;
        public ProjectManger(IUnitOfWork unitOfWork, IValidator<ProjectAddDto> validator
            )
        {
            this.unitOfWork = unitOfWork;
            this.validator = validator;
        }
        public async Task<GeneralResult> Add(ProjectAddDto projectAddDto)
        {
            var validationResult = await validator.ValidateAsync(projectAddDto);
            if (!validationResult.IsValid)
            {
                return new GeneralResult
                {
                    Status = false,
                    Errors = validationResult.Errors.Select(e => new ResultError
                    {
                        Code = e.ErrorMessage,
                        Message = e.ErrorMessage
                    }).ToArray()
                };
            }
            Project project = new Project
            {
                Project_Version = projectAddDto.Project_Version,
                Project_Description = projectAddDto.Project_Description,
                Project_Name = projectAddDto.Project_Name,
            };
            unitOfWork.ProjectRepo.Add(project);
            var result = await unitOfWork.SaveChangesAsync();
            if(result>0)
            {
                return new GeneralResult<ProjectAddDto>
                {

                    Status = true,
                    Data = projectAddDto
                };
            }
            else
            {
                return new GeneralResult<ProjectAddDto>
                {

                    Status = false,
                    Errors = []
                };
            }

        }

        public async Task<GeneralResult<List<ProjectShowDto>>> GetAllAsync()
        {
            var projects = await unitOfWork.ProjectRepo.GetAllAsync();
            var projectDto = projects.Select(p => new ProjectShowDto
            {
                Project_Description = p.Project_Description,
                Project_Name = p.Project_Name,
                Project_Version = p.Project_Version,
            }).ToList();

            return new GeneralResult<List<ProjectShowDto>>
            {
                Status = true,
                Data = projectDto

            };
        }

        public async Task<GeneralResult<ProjectShowWithDetailsDto>> GetByIdWithDetailsAsync(Guid id)
        {
            var project = await unitOfWork.ProjectRepo.GetByIdWithDetailsAsync(id);

            return new GeneralResult<ProjectShowWithDetailsDto>
            {
                Status = true,
                Data = new ProjectShowWithDetailsDto
                {
                    Project_Description = project.Project_Description,
                    Project_Name = project.Project_Name,
                    Project_Version = project.Project_Version,
                    Bugs = project.Bugs.Select(b => new BugDetails
                    {
                        Bug_Description = b.Bug_Description,
                        Bug_Name = b.Bug_Name,
                        Bug_Type = b.Bug_Type,
                        CreatedAt = b.CreatedAt,
                        priority = b.priority,
                        Status = b.Status

                    }).ToList(),
                }
            };
        }

        public async Task<GeneralResult> GetByNameAsync(string name)
        {
            var project = await unitOfWork.ProjectRepo.GetByNameAsync(name);
            ProjectShowDto projectShowDto = new ProjectShowDto
            {
                Project_Name = project.Project_Name,
                Project_Description = project.Project_Description,
                Project_Version = project.Project_Version,
            };
            return new GeneralResult<ProjectShowDto>
            {
                Status = true,
                Data = projectShowDto,
            };
        }
    }
}
