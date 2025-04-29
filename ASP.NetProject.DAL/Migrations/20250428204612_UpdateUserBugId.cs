using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BugTicketing.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserBugId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBugs_Users_Bug_Id",
                table: "UserBugs");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBugs_Users_User_Id",
                table: "UserBugs",
                column: "User_Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBugs_Users_User_Id",
                table: "UserBugs");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBugs_Users_Bug_Id",
                table: "UserBugs",
                column: "Bug_Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
