namespace BugTicketing.DAL
{
    public class UserRepo: GenericRepo<User> , IUserRepo
    {
        public UserRepo(Context context):base(context) { }
       
    }
}
