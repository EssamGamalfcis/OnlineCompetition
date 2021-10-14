using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineCompetition.MVC.Migrations
{
    public partial class CompetitionQuestionsAnswers2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                schema: "Identity",
                table: "CompetitionQuestionsAnswers");

            migrationBuilder.DropColumn(
                name: "DeleteDate",
                schema: "Identity",
                table: "CompetitionQuestionsAnswers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Identity",
                table: "CompetitionQuestionsAnswers");

            migrationBuilder.DropColumn(
                name: "NameAR",
                schema: "Identity",
                table: "CompetitionQuestionsAnswers");

            migrationBuilder.DropColumn(
                name: "NameEN",
                schema: "Identity",
                table: "CompetitionQuestionsAnswers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                schema: "Identity",
                table: "CompetitionQuestionsAnswers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDate",
                schema: "Identity",
                table: "CompetitionQuestionsAnswers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Identity",
                table: "CompetitionQuestionsAnswers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "NameAR",
                schema: "Identity",
                table: "CompetitionQuestionsAnswers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameEN",
                schema: "Identity",
                table: "CompetitionQuestionsAnswers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
