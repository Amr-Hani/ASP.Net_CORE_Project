using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BugTicketing.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAttachmentURL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attachment_Description",
                table: "attachments");

            migrationBuilder.RenameColumn(
                name: "Attachment_Image",
                table: "attachments",
                newName: "Attachment_URL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Attachment_URL",
                table: "attachments",
                newName: "Attachment_Image");

            migrationBuilder.AddColumn<string>(
                name: "Attachment_Description",
                table: "attachments",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }
    }
}
