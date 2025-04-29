using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketing.DAL
{
    public interface IProjectRepo : IGenericRepo<Project>
    {
        Task<Project> GetByNameAsync(string name);
        Task<Project> GetByIdWithDetailsAsync(Guid id);

    }
}
