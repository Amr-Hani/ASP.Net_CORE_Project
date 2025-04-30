using BugTicketing.DAL;

namespace BugTicketing.BL
{
    public class BugShowDto
    {
        public string Bug_Id { get; set; } = string.Empty;
        public string Bug_Name { get; set; } = string.Empty;
        public string Bug_Description { get; set; } = string.Empty;
        public string Bug_Type { get; set; } = string.Empty;

        public string priority { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
