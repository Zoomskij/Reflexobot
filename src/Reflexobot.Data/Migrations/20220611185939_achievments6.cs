using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reflexobot.Data.Migrations
{
    public partial class achievments6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Achievments_Students_StudentEntityGuid",
                table: "Achievments");

            migrationBuilder.DropIndex(
                name: "IX_Achievments_StudentEntityGuid",
                table: "Achievments");

            migrationBuilder.DropColumn(
                name: "StudentEntityGuid",
                table: "Achievments");

            migrationBuilder.AddColumn<Guid>(
                name: "StudentEntityGuid",
                table: "StudentAchievments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentAchievments_StudentEntityGuid",
                table: "StudentAchievments",
                column: "StudentEntityGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAchievments_Students_StudentEntityGuid",
                table: "StudentAchievments",
                column: "StudentEntityGuid",
                principalTable: "Students",
                principalColumn: "Guid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAchievments_Students_StudentEntityGuid",
                table: "StudentAchievments");

            migrationBuilder.DropIndex(
                name: "IX_StudentAchievments_StudentEntityGuid",
                table: "StudentAchievments");

            migrationBuilder.DropColumn(
                name: "StudentEntityGuid",
                table: "StudentAchievments");

            migrationBuilder.AddColumn<Guid>(
                name: "StudentEntityGuid",
                table: "Achievments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Achievments_StudentEntityGuid",
                table: "Achievments",
                column: "StudentEntityGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Achievments_Students_StudentEntityGuid",
                table: "Achievments",
                column: "StudentEntityGuid",
                principalTable: "Students",
                principalColumn: "Guid");
        }
    }
}
