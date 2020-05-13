using Microsoft.EntityFrameworkCore.Migrations;

namespace KnowledgeBase.Migrations
{
    public partial class MergedDataAndUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Themes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Subjects",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Themes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Subjects");
        }
    }
}
