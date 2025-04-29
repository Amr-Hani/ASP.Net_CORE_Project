namespace BugTicketing.DAL
{
    public class UserBug
    {
        public string User_Id { get; set; }
        public User User { get; set; }

        public string Bug_Id { get; set; }
        public Bug Bug { get; set; }
    }
}
