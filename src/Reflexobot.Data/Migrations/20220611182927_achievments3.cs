using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reflexobot.Data.Migrations
{
    public partial class achievments3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Achievmens_Students_StudentEntityGuid",
                table: "Achievmens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Achievmens",
                table: "Achievmens");

            migrationBuilder.RenameTable(
                name: "Achievmens",
                newName: "Achievments");

            migrationBuilder.RenameIndex(
                name: "IX_Achievmens_StudentEntityGuid",
                table: "Achievments",
                newName: "IX_Achievments_StudentEntityGuid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Achievments",
                table: "Achievments",
                column: "Guid");

            migrationBuilder.AddForeignKey(
                name: "FK_Achievments_Students_StudentEntityGuid",
                table: "Achievments",
                column: "StudentEntityGuid",
                principalTable: "Students",
                principalColumn: "Guid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Achievments_Students_StudentEntityGuid",
                table: "Achievments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Achievments",
                table: "Achievments");

            migrationBuilder.RenameTable(
                name: "Achievments",
                newName: "Achievmens");

            migrationBuilder.RenameIndex(
                name: "IX_Achievments_StudentEntityGuid",
                table: "Achievmens",
                newName: "IX_Achievmens_StudentEntityGuid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Achievmens",
                table: "Achievmens",
                column: "Guid");

            migrationBuilder.AddForeignKey(
                name: "FK_Achievmens_Students_StudentEntityGuid",
                table: "Achievmens",
                column: "StudentEntityGuid",
                principalTable: "Students",
                principalColumn: "Guid");
        }
    }
}
