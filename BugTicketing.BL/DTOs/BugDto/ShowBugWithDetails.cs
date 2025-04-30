using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketing.DAL;

namespace BugTicketing.BL
{
    public class ShowBugWithDetails
    {

        public string Bug_Id { get; set; }
        public string Bug_Name { get; set; } = string.Empty;
        public string Bug_Description { get; set; } = string.Empty;
        public string Bug_Type { get; set; } = string.Empty;

        public string priority { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<UserBug> UserBugs { get; set; } = [];
        public List<AttachmentBug> AttachmentBugs { get; set; } = [];
    }
    public class UserBug
    {
        public string Name { get; set; } = string.Empty;
    }
    public class AttachmentBug
    {
        public string Attachment_Name { get; set; } = string.Empty;
        public string Attachment_Description { get; set; } = string.Empty;

        public string Attachment_Image { get; set; } = string.Empty;
    }
}
