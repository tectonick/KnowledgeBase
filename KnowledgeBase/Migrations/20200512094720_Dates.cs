using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KnowledgeBase.Migrations
{
    public partial class Dates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Theme_Subjects_SubjectId",
                table: "Theme");

            migrationBuilder.DropColumn(
                name: "NextRepeat",
                table: "Theme");

            migrationBuilder.DropColumn(
                name: "TimesRepeated",
                table: "Theme");

            migrationBuilder.AlterColumn<int>(
                name: "SubjectId",
                table: "Theme",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Theme",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Theme_Subjects_SubjectId",
                table: "Theme",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Theme_Subjects_SubjectId",
                table: "Theme");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Theme");

            migrationBuilder.AlterColumn<int>(
                name: "SubjectId",
                table: "Theme",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<DateTime>(
                name: "NextRepeat",
                table: "Theme",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TimesRepeated",
                table: "Theme",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Theme_Subjects_SubjectId",
                table: "Theme",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
