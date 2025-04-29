using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketing.BL.Commen;
using BugTicketing.DAL;
using Microsoft.AspNetCore.Identity;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BugTicketing.BL
{
    public class UserBugMangerRepo : IUserBugMangerRepo
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<User> userManager;

        public UserBugMangerRepo(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
        }
        public async Task<GeneralResult> AddAsync(string id, UserBugAddDto userBugAddDto)
        {
            var bug = await unitOfWork.BugRepo.GetByIdAsync(id);
            //Console.WriteLine(bug.Bug_Name);
            if (bug is null)
            {
                return new GeneralResult
                {
                    Status = false,
                    Errors = new ResultError[]
                        {
                            new ResultError
                            {
                                Code = "404",
                                Message = "Bug is NotFound"
                            }
                    }
                };
            }
            Console.WriteLine(userBugAddDto.User_Id);
            var user = await userManager.FindByIdAsync(userBugAddDto.User_Id);
            if (user is null)
            {
                return new GeneralResult
                {
                    Status = false,
                    Errors = new ResultError[]
                        {
                            new ResultError
                            {
                                Code = "404",
                                Message = "User Is NotFound"
                            }
                    }
                };
            }


            var oldUserBug = await unitOfWork.UserBugRepo.GetUserBugByIdAsync(userBugAddDto.User_Id, id);
            if (oldUserBug != null)
            {
                return new GeneralResult
                {
                    Status = false,
                    Errors = new ResultError[]
                        {
                            new ResultError
                            {
                                Code = "400",
                                Message = "UserBug Is Exists"
                            }
                    }
                };
            }

            var userBug = new BugTicketing.DAL.UserBug
            {
                Bug_Id = bug.Bug_Id,
                User_Id = userBugAddDto.User_Id
            };


            unitOfWork.UserBugRepo.Add(userBug);
            var result = await unitOfWork.SaveChangesAsync();
            if(result > 0)
            {
                return new GeneralResult
                {
                    Status = true
                };
            }
            return new GeneralResult
            {
                Status = true
            };

        }

        public async Task<GeneralResult> DeleteAsync(string bugId, string userId)
        {
            var oldUserBug = await unitOfWork.UserBugRepo.GetUserBugByIdAsync(userId, bugId);
            if (oldUserBug == null)
            {
                return new GeneralResult
                {
                    Status = false,
                    Errors = new ResultError[]
                        {
                            new ResultError
                            {
                                Code = "404",
                                Message = "UserBug Not Found"
                            }
                    }
                };
            }

            unitOfWork.UserBugRepo.Remove(oldUserBug);
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0)
            {
                return new GeneralResult
                {
                    Status = true
                };
            }
            return new GeneralResult
            {
                Status = true
            };


        }
    }
}