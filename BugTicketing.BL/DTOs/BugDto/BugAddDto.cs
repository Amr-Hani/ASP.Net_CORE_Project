using BugTicketing.DAL;

namespace BugTicketing.BL
{
    public class BugAddDto
    {
        public string Bug_Id { get; set; } = string.Empty;
        public string Bug_Name { get; set; } = string.Empty;
        public string Bug_Description { get; set; } = string.Empty;
        public string Bug_Type { get; set; } = string.Empty;

        public BugPriority priority { get; set; }
        public BugStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid Project_Id { get; set; }

    }
}
