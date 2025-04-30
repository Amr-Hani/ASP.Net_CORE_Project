using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketing.BL
{
    public class ProjectShowDto
    {
        public Guid Project_Id { get; set; }
        public string Project_Name { get; set; } = string.Empty;
        public string Project_Description { get; set; } = string.Empty;
        public string Project_Version { get; set; } = string.Empty;

    }

}
