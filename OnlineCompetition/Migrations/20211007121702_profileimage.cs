using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineCompetition.MVC.Migrations
{
    public partial class profileimage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                schema: "Identity",
                table: "User");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                schema: "Identity",
                table: "User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                schema: "Identity",
                table: "User");

            migrationBuilder.AddColumn<byte[]>(
                name: "ProfilePicture",
                schema: "Identity",
                table: "User",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
