using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reflexobot.Data.Migrations
{
    public partial class newEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserNotifyIds");

            migrationBuilder.DropTable(
                name: "UserPersonIds");

            migrationBuilder.CreateTable(
                name: "StudentCourseIs",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    CourseGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCourseIs", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "StudentNotifyIds",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    NotifyGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentNotifyIds", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "StudentPersonIds",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentPersonIds", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "StudentLessonIds",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    LessonGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentCourseIdGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentLessonIds", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_StudentLessonIds_StudentCourseIs_StudentCourseIdGuid",
                        column: x => x.StudentCourseIdGuid,
                        principalTable: "StudentCourseIs",
                        principalColumn: "Guid");
                });

            migrationBuilder.CreateTable(
                name: "StudentTaskIds",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    TaskGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentLessonIdGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentTaskIds", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_StudentTaskIds_StudentLessonIds_StudentLessonIdGuid",
                        column: x => x.StudentLessonIdGuid,
                        principalTable: "StudentLessonIds",
                        principalColumn: "Guid");
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentLessonIds_StudentCourseIdGuid",
                table: "StudentLessonIds",
                column: "StudentCourseIdGuid");

            migrationBuilder.CreateIndex(
                name: "IX_StudentTaskIds_StudentLessonIdGuid",
                table: "StudentTaskIds",
                column: "StudentLessonIdGuid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentNotifyIds");

            migrationBuilder.DropTable(
                name: "StudentPersonIds");

            migrationBuilder.DropTable(
                name: "StudentTaskIds");

            migrationBuilder.DropTable(
                name: "StudentLessonIds");

            migrationBuilder.DropTable(
                name: "StudentCourseIs");

            migrationBuilder.CreateTable(
                name: "UserNotifyIds",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NotifyGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserNotifyIds", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "UserPersonIds",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPersonIds", x => x.Guid);
                });
        }
    }
}
