using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BugTicketing.DAL
{
    public class ProjectConfig : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(p=>p.Project_Id);
            builder.Property(p => p.Project_Name).HasColumnType("nvarchar(50)").HasMaxLength(50);
            builder.Property(p => p.Project_Description).HasColumnType("nvarchar(150)").HasMaxLength(150);
            builder.Property(p => p.Project_Version).HasColumnType("nvarchar(50)").HasMaxLength(50);

            builder.HasMany(b => b.Bugs)
                .WithOne(b => b.Project)
                .HasForeignKey(b => b.Project_Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
