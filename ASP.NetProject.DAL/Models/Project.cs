namespace BugTicketing.DAL
{
    public class Project
    {
        public Guid Project_Id { get; set; }
        public string Project_Name { get; set; } = string.Empty;
        public string Project_Description { get; set; } = string.Empty;
        public string Project_Version { get; set; } = string.Empty;

        // ----------------------- releation between Bug ------------------------- \\
        // -----------------------      One To Many      ------------------------ \\
        public virtual ICollection<Bug> Bugs { get; set; } = new HashSet<Bug>();

    }
}
