using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reflexobot.Data.Migrations
{
    public partial class fixForNotifyIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotifyId",
                table: "UserNotifyIds");

            migrationBuilder.AddColumn<Guid>(
                name: "NotifyGuid",
                table: "UserNotifyIds",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotifyGuid",
                table: "UserNotifyIds");

            migrationBuilder.AddColumn<int>(
                name: "NotifyId",
                table: "UserNotifyIds",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
