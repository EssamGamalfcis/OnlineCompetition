using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineCompetition.MVC.Migrations
{
    public partial class CompetationModelUpdate3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompetitionsUsers_User_StudentUserId1",
                schema: "Identity",
                table: "CompetitionsUsers");

            migrationBuilder.DropIndex(
                name: "IX_CompetitionsUsers_StudentUserId1",
                schema: "Identity",
                table: "CompetitionsUsers");

            migrationBuilder.DropColumn(
                name: "StudentUserId1",
                schema: "Identity",
                table: "CompetitionsUsers");

            migrationBuilder.AlterColumn<string>(
                name: "StudentUserId",
                schema: "Identity",
                table: "CompetitionsUsers",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionsUsers_StudentUserId",
                schema: "Identity",
                table: "CompetitionsUsers",
                column: "StudentUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompetitionsUsers_User_StudentUserId",
                schema: "Identity",
                table: "CompetitionsUsers",
                column: "StudentUserId",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompetitionsUsers_User_StudentUserId",
                schema: "Identity",
                table: "CompetitionsUsers");

            migrationBuilder.DropIndex(
                name: "IX_CompetitionsUsers_StudentUserId",
                schema: "Identity",
                table: "CompetitionsUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "StudentUserId",
                schema: "Identity",
                table: "CompetitionsUsers",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentUserId1",
                schema: "Identity",
                table: "CompetitionsUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionsUsers_StudentUserId1",
                schema: "Identity",
                table: "CompetitionsUsers",
                column: "StudentUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CompetitionsUsers_User_StudentUserId1",
                schema: "Identity",
                table: "CompetitionsUsers",
                column: "StudentUserId1",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
