using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reflexobot.Data.Migrations
{
    public partial class achievments2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Achievment_Students_StudentEntityGuid",
                table: "Achievment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Achievment",
                table: "Achievment");

            migrationBuilder.RenameTable(
                name: "Achievment",
                newName: "Achievmens");

            migrationBuilder.RenameIndex(
                name: "IX_Achievment_StudentEntityGuid",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Achievmens_Students_StudentEntityGuid",
                table: "Achievmens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Achievmens",
                table: "Achievmens");

            migrationBuilder.RenameTable(
                name: "Achievmens",
                newName: "Achievment");

            migrationBuilder.RenameIndex(
                name: "IX_Achievmens_StudentEntityGuid",
                table: "Achievment",
                newName: "IX_Achievment_StudentEntityGuid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Achievment",
                table: "Achievment",
                column: "Guid");

            migrationBuilder.AddForeignKey(
                name: "FK_Achievment_Students_StudentEntityGuid",
                table: "Achievment",
                column: "StudentEntityGuid",
                principalTable: "Students",
                principalColumn: "Guid");
        }
    }
}
