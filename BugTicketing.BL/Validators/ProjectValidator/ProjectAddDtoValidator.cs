using BugTicketing.DAL;
using FluentValidation;

namespace BugTicketing.BL
{
    public class ProjectAddDtoValidator : AbstractValidator<ProjectAddDto>
    {
        private readonly IUnitOfWork unitOfWork;
        public ProjectAddDtoValidator(IUnitOfWork unitOfWork )
        {
            RuleFor(p => p.Project_Name)
                 .NotEmpty()
                 .WithMessage("Project_Name Shouldn't ne Empty")
                 .MaximumLength(50)
                 .WithMessage("Project_Name Shouldn't Exede 50 Character")
                  .MustAsync(CheckUserNameIsUnique)
                   .WithMessage("This Project_Name already Exists");

            RuleFor(p => p.Project_Description)
              .NotEmpty()
              .WithMessage("Project_Description Shouldn't ne Empty")
                 .MaximumLength(50)
                 .WithMessage("Project_Description Shouldn't Exede 50 Character");

            RuleFor(p => p.Project_Version)
               .NotEmpty()
              .WithMessage("Project_Version Shouldn't ne Empty")
                 .MaximumLength(50)
                 .WithMessage("Project_Version Shouldn't Exede 50 Character");
            this.unitOfWork = unitOfWork;
    
        }
        private async Task<bool> CheckUserNameIsUnique(string arg, CancellationToken token)
        {
            var user = await unitOfWork.ProjectRepo.GetByNameAsync(arg);
            if (user == null)
            {
                return true;
            }
            return false;

        }
    }
}
