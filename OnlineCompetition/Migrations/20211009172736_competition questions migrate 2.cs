using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineCompetition.MVC.Migrations
{
    public partial class competitionquestionsmigrate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RightAnswer",
                schema: "Identity",
                table: "CompetitionsQuestions");

            migrationBuilder.AddColumn<long>(
                name: "RightCompetitionQuestionAnswerId",
                schema: "Identity",
                table: "CompetitionsQuestions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Sort",
                schema: "Identity",
                table: "CompetitionsQuestions",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RightCompetitionQuestionAnswerId",
                schema: "Identity",
                table: "CompetitionsQuestions");

            migrationBuilder.DropColumn(
                name: "Sort",
                schema: "Identity",
                table: "CompetitionsQuestions");

            migrationBuilder.AddColumn<long>(
                name: "RightAnswer",
                schema: "Identity",
                table: "CompetitionsQuestions",
                type: "bigint",
                nullable: true);
        }
    }
}
