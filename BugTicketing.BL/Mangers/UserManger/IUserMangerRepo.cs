using BugTicketing.BL.Commen;

namespace BugTicketing.BL
{
    public interface IUserMangerRepo
    {
        public Task<GeneralResult> Regisetr(Register register);
        public Task<GeneralResult> Login(Login login);
    }
}
