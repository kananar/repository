using Microsoft.EntityFrameworkCore.Migrations;

namespace EduHome.Migrations
{
    public partial class changeComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppuserId",
                table: "Comments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AppuserId1",
                table: "Comments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AppuserId1",
                table: "Comments",
                column: "AppuserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_AppuserId1",
                table: "Comments",
                column: "AppuserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_AppuserId1",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_AppuserId1",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "AppuserId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "AppuserId1",
                table: "Comments");
        }
    }
}
