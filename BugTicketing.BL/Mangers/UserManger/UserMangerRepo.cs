using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BugTicketing.BL.Commen;
using BugTicketing.DAL;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BugTicketing.BL
{
    public class UserMangerRepo : IUserMangerRepo
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<User> userManager;
        private readonly IConfiguration configuration;
        private readonly IValidator<Register> validator;

        public UserMangerRepo(IUnitOfWork unitOfWork,
                              UserManager<User> userManager,
                              IConfiguration configuration,
                              IValidator<Register> validator
            )
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
            this.configuration = configuration;
            this.validator = validator;
        }

       

        public async Task<GeneralResult> Regisetr(Register register)
        {
            var validationResult = await validator.ValidateAsync(register);
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


            var user = new User
            {
                UserName = register.Username,
                Email = register.Email,
                Role = Enum.Parse<UserEnum>(register.Role.Trim(), ignoreCase: true)
            };

            var creationResult = await userManager.CreateAsync(user, register.Password);
            if (!creationResult.Succeeded)
            {
                var errors = creationResult.Errors
                    .Select(e => e.Description)
                    .ToList();

                return new GeneralResult
                {
                    Status = false,
                    Errors = creationResult.Errors.Select(e => new ResultError
                    {
                        Code = e.Code,
                        Message = e.Description
                    }).ToArray()
                };
            }
            var claims = new List<Claim>
                {
                    // Best Practices
                    new (ClaimTypes.NameIdentifier, user.Id),
                    new (ClaimTypes.Email, user.Email),
                    new (ClaimTypes.Role,user.Role.ToString())
                };
            await userManager.AddClaimsAsync(user, claims);
            return new GeneralResult
            {
                Status = true,
                Errors = []
            };
        }

        private TokenDto GenerateToken(List<Claim> claims)
        {
            var secretKey = configuration.GetSection("ScretKey").Value;

            var secretKeyInBytes = Encoding.UTF8.GetBytes(secretKey);
            var key = new SymmetricSecurityKey(secretKeyInBytes);

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddHours(1),
                claims: claims,
                signingCredentials: new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256
                )
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new TokenDto
            {
                Token = tokenString,
                ExpirDate = token.ValidTo
            };
        }

        public async Task<GeneralResult> Login(Login login)
        {
            var user = await userManager.FindByEmailAsync(login.Email);
            if (user is null)
            {
                 return new GeneralResult
                {
                    Status = false,
                    Errors =  [new ResultError
                    {
                        Code = "400",
                        Message = "BadRequest, email is not found"
                    } ]
                };
            }

            var isPasswordValid = await userManager.CheckPasswordAsync(
                user,
                login.Password);

            if (!isPasswordValid)
            {

                return new GeneralResult
                {
                    Status = false,
                    Errors = [new ResultError
                    {
                        Code = "400",
                        Message = "BadRequest, passowrd in correct"
                    } ]
                };
            }
            var claims = await userManager.GetClaimsAsync(user);
            var tokenDto = GenerateToken(claims.ToList());
            return new GeneralResult<TokenDto>
            {
                Status = true,
                Data = tokenDto,
            };
        }

    }
}
