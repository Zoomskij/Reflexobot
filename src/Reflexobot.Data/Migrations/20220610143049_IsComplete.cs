using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reflexobot.Data.Migrations
{
    public partial class IsComplete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentLessonIds_StudentCourseIs_StudentCourseIdGuid",
                table: "StudentLessonIds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentCourseIs",
                table: "StudentCourseIs");

            migrationBuilder.RenameTable(
                name: "StudentCourseIs",
                newName: "StudentCourseIds");

            migrationBuilder.AddColumn<bool>(
                name: "IsComplete",
                table: "StudentTaskIds",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsComplete",
                table: "StudentLessonIds",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentCourseIds",
                table: "StudentCourseIds",
                column: "Guid");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentLessonIds_StudentCourseIds_StudentCourseIdGuid",
                table: "StudentLessonIds",
                column: "StudentCourseIdGuid",
                principalTable: "StudentCourseIds",
                principalColumn: "Guid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentLessonIds_StudentCourseIds_StudentCourseIdGuid",
                table: "StudentLessonIds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentCourseIds",
                table: "StudentCourseIds");

            migrationBuilder.DropColumn(
                name: "IsComplete",
                table: "StudentTaskIds");

            migrationBuilder.DropColumn(
                name: "IsComplete",
                table: "StudentLessonIds");

            migrationBuilder.RenameTable(
                name: "StudentCourseIds",
                newName: "StudentCourseIs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentCourseIs",
                table: "StudentCourseIs",
                column: "Guid");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentLessonIds_StudentCourseIs_StudentCourseIdGuid",
                table: "StudentLessonIds",
                column: "StudentCourseIdGuid",
                principalTable: "StudentCourseIs",
                principalColumn: "Guid");
        }
    }
}
