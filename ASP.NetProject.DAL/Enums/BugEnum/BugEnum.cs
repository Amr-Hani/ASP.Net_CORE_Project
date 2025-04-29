namespace BugTicketing.DAL
{
    [Flags]
    public enum BugStatus
    {
        Opened = 0,
        InProcess = 1,
        Resolved = 2
    }

    [Flags]
    public enum BugPriority
    {
        LOW = 0,
        MEDIUM = 1,
        HIGH = 2
    }
}
