using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineCompetition.MVC.Migrations
{
    public partial class competitionquestionsmigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                schema: "Identity",
                table: "CompetitionsQuestionsAnswers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDate",
                schema: "Identity",
                table: "CompetitionsQuestionsAnswers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Identity",
                table: "CompetitionsQuestionsAnswers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "NameAR",
                schema: "Identity",
                table: "CompetitionsQuestionsAnswers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameEN",
                schema: "Identity",
                table: "CompetitionsQuestionsAnswers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "AnswerType",
                schema: "Identity",
                table: "CompetitionsQuestions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "RightAnswer",
                schema: "Identity",
                table: "CompetitionsQuestions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                schema: "Identity",
                table: "CompetitionsQuestionsAnswers");

            migrationBuilder.DropColumn(
                name: "DeleteDate",
                schema: "Identity",
                table: "CompetitionsQuestionsAnswers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Identity",
                table: "CompetitionsQuestionsAnswers");

            migrationBuilder.DropColumn(
                name: "NameAR",
                schema: "Identity",
                table: "CompetitionsQuestionsAnswers");

            migrationBuilder.DropColumn(
                name: "NameEN",
                schema: "Identity",
                table: "CompetitionsQuestionsAnswers");

            migrationBuilder.DropColumn(
                name: "AnswerType",
                schema: "Identity",
                table: "CompetitionsQuestions");

            migrationBuilder.DropColumn(
                name: "RightAnswer",
                schema: "Identity",
                table: "CompetitionsQuestions");
        }
    }
}
