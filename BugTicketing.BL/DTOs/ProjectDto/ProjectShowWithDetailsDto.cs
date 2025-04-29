using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketing.DAL;

namespace BugTicketing.BL
{
    public class ProjectShowWithDetailsDto
    {
        public string Project_Name { get; set; } = string.Empty;
        public string Project_Description { get; set; } = string.Empty;
        public string Project_Version { get; set; } = string.Empty;
        public List<BugDetails> Bugs { get; set; } = [];
    }

    public class BugDetails
    {
        public string Bug_Name { get; set; } = string.Empty;
        public string Bug_Description { get; set; } = string.Empty;
        public string Bug_Type { get; set; } = string.Empty;

        public BugPriority priority { get; set; }
        public BugStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
