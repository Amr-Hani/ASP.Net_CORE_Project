using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketing.BL.Commen;

namespace BugTicketing.BL
{
    public interface IUserBugMangerRepo
    {
        public Task<GeneralResult> AddAsync(string id,UserBugAddDto userBugAddDto);
        public Task<GeneralResult> DeleteAsync(string bugId,string userId);

    }
}
