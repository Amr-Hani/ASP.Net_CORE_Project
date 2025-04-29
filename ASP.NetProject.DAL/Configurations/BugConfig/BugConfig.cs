using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BugTicketing.DAL
{
    public class BugConfig : IEntityTypeConfiguration<Bug>
    {
        public void Configure(EntityTypeBuilder<Bug> builder)
        {
            builder.HasKey(b=>b.Bug_Id);
            builder.Property(b => b.Bug_Name).HasColumnType("nvarchar(50)").HasMaxLength(50);
            builder.Property(b => b.Bug_Description).HasColumnType("nvarchar(150)").HasMaxLength(150);
            builder.Property(b => b.Bug_Type).HasColumnType("nvarchar(50)").HasMaxLength(50);
            builder.Property(b => b.Status).HasColumnType("nvarchar(50)").HasMaxLength(50);
            builder.Property(b => b.priority).HasColumnType("nvarchar(50)").HasMaxLength(50);


            builder.Property(b => b.Status)
                 .HasConversion(
                     v => v.ToString(),                                 // to DB (as string)
                     v => (BugStatus)Enum.Parse(typeof(BugStatus), v)); // from DB

            builder.Property(b => b.priority)
                 .HasConversion(
                     v => v.ToString(),                                     // to DB (as string)
                     v => (BugPriority)Enum.Parse(typeof(BugPriority), v)); // from DB
        }
    }
}
