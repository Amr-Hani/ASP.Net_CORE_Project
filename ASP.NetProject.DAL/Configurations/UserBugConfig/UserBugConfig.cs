using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BugTicketing.DAL
{
    public class UserBugConfig : IEntityTypeConfiguration<UserBug>
    {
        public void Configure(EntityTypeBuilder<UserBug> builder)
        {
            builder.HasKey(ub =>new { ub.User_Id , ub.Bug_Id });

            builder.HasOne(ub => ub.User)
                   .WithMany(u => u.Bugs)
                   .HasForeignKey(ub => ub.Bug_Id);
            
            builder.HasOne(ub => ub.User)
                   .WithMany(u => u.Bugs)
                   .HasForeignKey(ub => ub.User_Id);

        }
    }
}
