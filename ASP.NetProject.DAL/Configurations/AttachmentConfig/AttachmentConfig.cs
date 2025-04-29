using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BugTicketing.DAL
{
    public class AttachmentConfig : IEntityTypeConfiguration<Attachment>
    {
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            builder.HasKey(a => a.Attachment_id);
            builder.Property(a => a.Attachment_Name).HasColumnType("nvarchar(50)").HasMaxLength(50);
            builder.Property(a => a.Attachment_URL).HasColumnType("nvarchar(250)").HasMaxLength(250);

            builder.HasOne(b=>b.Bug)
                .WithMany(a=>a.Attachments)
                .HasForeignKey(a=>a.Bug_Id)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
