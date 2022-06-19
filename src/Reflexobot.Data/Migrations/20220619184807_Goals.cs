using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reflexobot.Data.Migrations
{
    public partial class Goals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GoalGuid",
                table: "Tasks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GoalGuid",
                table: "Students",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GoalGuid",
                table: "Lessons",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GoalGuid",
                table: "Courses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => x.Guid);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_GoalGuid",
                table: "Tasks",
                column: "GoalGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Students_GoalGuid",
                table: "Students",
                column: "GoalGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_GoalGuid",
                table: "Lessons",
                column: "GoalGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_GoalGuid",
                table: "Courses",
                column: "GoalGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Goals_GoalGuid",
                table: "Courses",
                column: "GoalGuid",
                principalTable: "Goals",
                principalColumn: "Guid");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Goals_GoalGuid",
                table: "Lessons",
                column: "GoalGuid",
                principalTable: "Goals",
                principalColumn: "Guid");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Goals_GoalGuid",
                table: "Students",
                column: "GoalGuid",
                principalTable: "Goals",
                principalColumn: "Guid");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Goals_GoalGuid",
                table: "Tasks",
                column: "GoalGuid",
                principalTable: "Goals",
                principalColumn: "Guid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Goals_GoalGuid",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Goals_GoalGuid",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Goals_GoalGuid",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Goals_GoalGuid",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "Goals");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_GoalGuid",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Students_GoalGuid",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_GoalGuid",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Courses_GoalGuid",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "GoalGuid",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "GoalGuid",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "GoalGuid",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "GoalGuid",
                table: "Courses");
        }
    }
}
