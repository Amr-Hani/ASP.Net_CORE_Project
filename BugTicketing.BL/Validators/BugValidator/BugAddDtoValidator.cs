using BugTicketing.DAL;
using FluentValidation;

namespace BugTicketing.BL
{
    public class BugAddDtoValidator : AbstractValidator<BugAddDto>
    {
        private readonly IUnitOfWork unitOfWork;

        public BugAddDtoValidator(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

            //RuleFor(b => b.Bug_Id)
            //    .NotEmpty()
            //    .WithMessage("Bug_Id should not be empty.")
            //    .MustAsync(IsBugIdUnique)
            //    .WithMessage("Bug_Id already exists.");

            RuleFor(b => b.Bug_Name)
                .NotEmpty()
                .WithMessage("Bug_Name should not be empty.")
                .MaximumLength(100)
                .WithMessage("Bug_Name should not exceed 100 characters.");

            RuleFor(b => b.Bug_Description)
                .NotEmpty()
                .WithMessage("Bug_Description should not be empty.")
                .MaximumLength(250)
                .WithMessage("Bug_Description should not exceed 250 characters.");

            RuleFor(b => b.Bug_Type)
                .NotEmpty()
                .WithMessage("Bug_Type should not be empty.")
                .MaximumLength(50)
                .WithMessage("Bug_Type should not exceed 50 characters.");

            RuleFor(b => b.priority)
                .IsInEnum()
                .WithMessage("Priority must be a valid value from BugPriority enum.");

            RuleFor(b => b.Status)
                .IsInEnum()
                .WithMessage("Status must be a valid value from BugStatus enum.");


        }
        private async Task<bool> IsBugIdUnique(string arg, CancellationToken token)
        {
            var bug = await unitOfWork.BugRepo.GetByIdAsync(arg);
            if (bug == null)
            {
                return true;
            }
            return false;

        }
    }
}

