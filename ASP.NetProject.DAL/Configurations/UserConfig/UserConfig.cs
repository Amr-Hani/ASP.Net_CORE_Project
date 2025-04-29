using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BugTicketing.DAL
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.Property(b => b.Role)
                 .HasConversion(
                     v => v.ToString(),                               // to DB (as string)
                     v => (UserEnum)Enum.Parse(typeof(UserEnum), v)); // from DB
        }
    }
}
