using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BugTicketing.DAL
{
    public class ProjrctRepo : GenericRepo<Project>, IProjectRepo
    {
        private readonly Context context;

        public ProjrctRepo(Context context) : base(context)
        {
            this.context = context;
        }

        public async Task<Project> GetByIdWithDetailsAsync(Guid id)
        {
            return await context.Set<Project>().Include(p => p.Bugs).FirstOrDefaultAsync(p => p.Project_Id == id);
        }

        public async Task<Project> GetByNameAsync(string name)
        {
            return await context.Set<Project>().FirstOrDefaultAsync(p => p.Project_Name == name);
        }
    }
}
