
using BugTicketing.BL;
using BugTicketing.DAL;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace BugTicketing.BL
{
    public class UserRegisterDTOValidator : AbstractValidator<Register>
    {
        private readonly UserManager<User> _userManager;
        public UserRegisterDTOValidator(UserManager<User> userManager)
        {
            _userManager = userManager;
            RuleFor(u => u.Username)
                    .NotEmpty()
                    .WithMessage("Name Shouldn't ne Empty")
                    .MaximumLength(20)
                    .WithMessage("Name Shouldn't Exede 20 Character");
            RuleFor(u => u.Email)
              .NotEmpty()
              .WithMessage("Email shouldn't be empty")
              .EmailAddress()
              .WithMessage("Invalid email format")
              .MustAsync(CheckUserEmailIsUnique)
              .WithMessage("This Email already Exists");

            RuleFor(u => u.Role)
              .IsEnumName(typeof(UserEnum))
              .WithMessage("Invalid role selected");

            RuleFor(u => u.Password)
             .NotEmpty()
             .WithMessage("Password shouldn't be empty");
        }

        private async Task<bool> CheckUserEmailIsUnique(string arg, CancellationToken token)
        {
            var user = await _userManager.FindByEmailAsync(arg);
            if (user == null)
            {
                return true;
            }
            return false;

        }
    }
}