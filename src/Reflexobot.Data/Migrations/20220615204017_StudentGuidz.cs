using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reflexobot.Data.Migrations
{
    public partial class StudentGuidz : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "StudentTaskIds");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "StudentPersonIds");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "StudentNotifyIds");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "StudentLessonIds");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "StudentCourseIds");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "StudentAchievments");

            migrationBuilder.AddColumn<Guid>(
                name: "StudentGuid",
                table: "StudentTaskIds",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "StudentGuid",
                table: "StudentPersonIds",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "StudentGuid",
                table: "StudentNotifyIds",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "StudentGuid",
                table: "StudentLessonIds",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "StudentGuid",
                table: "StudentCourseIds",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "StudentGuid",
                table: "StudentAchievments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentGuid",
                table: "StudentTaskIds");

            migrationBuilder.DropColumn(
                name: "StudentGuid",
                table: "StudentPersonIds");

            migrationBuilder.DropColumn(
                name: "StudentGuid",
                table: "StudentNotifyIds");

            migrationBuilder.DropColumn(
                name: "StudentGuid",
                table: "StudentLessonIds");

            migrationBuilder.DropColumn(
                name: "StudentGuid",
                table: "StudentCourseIds");

            migrationBuilder.DropColumn(
                name: "StudentGuid",
                table: "StudentAchievments");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "StudentTaskIds",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "StudentPersonIds",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "StudentNotifyIds",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "StudentLessonIds",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "StudentCourseIds",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "StudentAchievments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
