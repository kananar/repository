using Microsoft.EntityFrameworkCore.Migrations;

namespace EduHome.Migrations
{
    public partial class convertinttostring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_AppuserId1",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_AppuserId1",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "AppuserId1",
                table: "Comments");

            migrationBuilder.AlterColumn<string>(
                name: "AppuserId",
                table: "Comments",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AppuserId",
                table: "Comments",
                column: "AppuserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_AppuserId",
                table: "Comments",
                column: "AppuserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_AppuserId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_AppuserId",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "AppuserId",
                table: "Comments",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppuserId1",
                table: "Comments",
                type: "nvarchar(450)",
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
    }
}
