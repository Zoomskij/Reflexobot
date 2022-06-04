using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reflexobot.Data.Migrations
{
    public partial class removed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonPhrases_Persons_PersonGuid",
                table: "PersonPhrases");

            migrationBuilder.DropIndex(
                name: "IX_PersonPhrases_PersonGuid",
                table: "PersonPhrases");

            migrationBuilder.DropColumn(
                name: "PersonGuid",
                table: "PersonPhrases");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PersonGuid",
                table: "PersonPhrases",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_PersonPhrases_PersonGuid",
                table: "PersonPhrases",
                column: "PersonGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonPhrases_Persons_PersonGuid",
                table: "PersonPhrases",
                column: "PersonGuid",
                principalTable: "Persons",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
