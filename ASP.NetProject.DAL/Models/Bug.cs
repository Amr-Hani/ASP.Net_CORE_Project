namespace BugTicketing.DAL
{
    public class Bug
    {
        public string Bug_Id { get; set; }
        public string Bug_Name { get; set; } = string.Empty;
        public string Bug_Description { get; set; } = string.Empty;
        public string Bug_Type { get; set; } = string.Empty;

        public BugPriority priority { get; set; }
        public BugStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }


        // ----------------------- releation between User ------------------------- \\
        // -----------------------       Many To Many      ------------------------ \\
        public virtual ICollection<UserBug> Users { get; set; } = new HashSet<UserBug>();

        // ----------------------- releation between Project ------------------------- \\
        // -----------------------        One To Many        ------------------------- \\
        public Guid Project_Id { get; set; }
        public Project Project { get; set; }

        // ----------------------- releation between Attachment ------------------------- \\
        // -----------------------       One To Many      ------------------------ \\
        public virtual ICollection<Attachment> Attachments { get; set; } = new HashSet<Attachment>();


    }
}
