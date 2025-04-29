using Microsoft.AspNetCore.Identity;

namespace BugTicketing.DAL
{
    public class User: IdentityUser
    {
        public UserEnum Role { get; set; }

        // hena relations
        // ----------------------- releation between Bug ------------------------- \\
        // -----------------------      Many To Man      ------------------------- \\
        public virtual ICollection<UserBug> Bugs { get; set; } = new HashSet<UserBug>();
    }
}
