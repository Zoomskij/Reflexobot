using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reflexobot.Data.Migrations
{
    public partial class key : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonPhrases",
                table: "PersonPhrases");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonPhrases",
                table: "PersonPhrases",
                column: "Guid");

            migrationBuilder.CreateIndex(
                name: "IX_PersonPhrases_Id",
                table: "PersonPhrases",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonPhrases",
                table: "PersonPhrases");

            migrationBuilder.DropIndex(
                name: "IX_PersonPhrases_Id",
                table: "PersonPhrases");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonPhrases",
                table: "PersonPhrases",
                column: "Id");
        }
    }
}
