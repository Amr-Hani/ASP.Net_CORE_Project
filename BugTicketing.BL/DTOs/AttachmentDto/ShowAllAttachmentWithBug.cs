
using BugTicketing.DAL;

namespace BugTicketing.BL
{
    public class ShowAllAttachmentWithBug
    {

        public string Bug_Id { get; set; }
        public string Bug_Name { get; set; } = string.Empty;
        public string Bug_Description { get; set; } = string.Empty;
        public string Bug_Type { get; set; } = string.Empty;

        public BugPriority priority { get; set; }
        public BugStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<AttachmentBugg> AttachmentBugs { get; set; } = [];
    }
   
    public class AttachmentBugg
    {
        public Guid Attachment_Id { get; set; } 
        public string Attachment_Name { get; set; } = string.Empty;

        public string Attachment_URL { get; set; } = string.Empty;
    }
}

