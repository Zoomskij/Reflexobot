using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reflexobot.Data.Migrations
{
    public partial class personId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonPhrases_Persons_Id",
                table: "PersonPhrases");

            migrationBuilder.DropIndex(
                name: "IX_PersonPhrases_Id",
                table: "PersonPhrases");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PersonPhrases");

            migrationBuilder.CreateIndex(
                name: "IX_PersonPhrases_PersonId",
                table: "PersonPhrases",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonPhrases_Persons_PersonId",
                table: "PersonPhrases",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonPhrases_Persons_PersonId",
                table: "PersonPhrases");

            migrationBuilder.DropIndex(
                name: "IX_PersonPhrases_PersonId",
                table: "PersonPhrases");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "PersonPhrases",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PersonPhrases_Id",
                table: "PersonPhrases",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonPhrases_Persons_Id",
                table: "PersonPhrases",
                column: "Id",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
