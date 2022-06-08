using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reflexobot.Data.Migrations
{
    public partial class updateTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupEntity_Courses_CourseGuid",
                table: "GroupEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentEntity_GroupEntity_GroupGuid",
                table: "StudentEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentEntity",
                table: "StudentEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupEntity",
                table: "GroupEntity");

            migrationBuilder.RenameTable(
                name: "StudentEntity",
                newName: "Students");

            migrationBuilder.RenameTable(
                name: "GroupEntity",
                newName: "Groups");

            migrationBuilder.RenameIndex(
                name: "IX_StudentEntity_GroupGuid",
                table: "Students",
                newName: "IX_Students_GroupGuid");

            migrationBuilder.RenameIndex(
                name: "IX_GroupEntity_CourseGuid",
                table: "Groups",
                newName: "IX_Groups_CourseGuid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "Guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Groups",
                table: "Groups",
                column: "Guid");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Courses_CourseGuid",
                table: "Groups",
                column: "CourseGuid",
                principalTable: "Courses",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Groups_GroupGuid",
                table: "Students",
                column: "GroupGuid",
                principalTable: "Groups",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Courses_CourseGuid",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Groups_GroupGuid",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Groups",
                table: "Groups");

            migrationBuilder.RenameTable(
                name: "Students",
                newName: "StudentEntity");

            migrationBuilder.RenameTable(
                name: "Groups",
                newName: "GroupEntity");

            migrationBuilder.RenameIndex(
                name: "IX_Students_GroupGuid",
                table: "StudentEntity",
                newName: "IX_StudentEntity_GroupGuid");

            migrationBuilder.RenameIndex(
                name: "IX_Groups_CourseGuid",
                table: "GroupEntity",
                newName: "IX_GroupEntity_CourseGuid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentEntity",
                table: "StudentEntity",
                column: "Guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupEntity",
                table: "GroupEntity",
                column: "Guid");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupEntity_Courses_CourseGuid",
                table: "GroupEntity",
                column: "CourseGuid",
                principalTable: "Courses",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentEntity_GroupEntity_GroupGuid",
                table: "StudentEntity",
                column: "GroupGuid",
                principalTable: "GroupEntity",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
