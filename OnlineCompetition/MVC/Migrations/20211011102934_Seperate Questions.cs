using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineCompetition.MVC.Migrations
{
    public partial class SeperateQuestions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompetitionsQuestions_Competitions_CompetitionId",
                schema: "Identity",
                table: "CompetitionsQuestions");

            migrationBuilder.DropIndex(
                name: "IX_CompetitionsQuestions_CompetitionId",
                schema: "Identity",
                table: "CompetitionsQuestions");

            migrationBuilder.DropColumn(
                name: "CompetitionId",
                schema: "Identity",
                table: "CompetitionsQuestions");

            migrationBuilder.AddColumn<long>(
                name: "CompetitionsId",
                schema: "Identity",
                table: "QuestionsAnswers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsAnswers_CompetitionsId",
                schema: "Identity",
                table: "QuestionsAnswers",
                column: "CompetitionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionsAnswers_Competitions_CompetitionsId",
                schema: "Identity",
                table: "QuestionsAnswers",
                column: "CompetitionsId",
                principalSchema: "Identity",
                principalTable: "Competitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionsAnswers_Competitions_CompetitionsId",
                schema: "Identity",
                table: "QuestionsAnswers");

            migrationBuilder.DropIndex(
                name: "IX_QuestionsAnswers_CompetitionsId",
                schema: "Identity",
                table: "QuestionsAnswers");

            migrationBuilder.DropColumn(
                name: "CompetitionsId",
                schema: "Identity",
                table: "QuestionsAnswers");

            migrationBuilder.AddColumn<long>(
                name: "CompetitionId",
                schema: "Identity",
                table: "CompetitionsQuestions",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionsQuestions_CompetitionId",
                schema: "Identity",
                table: "CompetitionsQuestions",
                column: "CompetitionId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompetitionsQuestions_Competitions_CompetitionId",
                schema: "Identity",
                table: "CompetitionsQuestions",
                column: "CompetitionId",
                principalSchema: "Identity",
                principalTable: "Competitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
