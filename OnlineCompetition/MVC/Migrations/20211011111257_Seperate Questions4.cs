using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineCompetition.MVC.Migrations
{
    public partial class SeperateQuestions4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionsAnswers_Questions_CompetitionsQuestionsId",
                schema: "Identity",
                table: "QuestionsAnswers");

            migrationBuilder.DropIndex(
                name: "IX_QuestionsAnswers_CompetitionsQuestionsId",
                schema: "Identity",
                table: "QuestionsAnswers");

            migrationBuilder.DropColumn(
                name: "CompetitionsQuestionsId",
                schema: "Identity",
                table: "QuestionsAnswers");

            migrationBuilder.AddColumn<long>(
                name: "QuestionId",
                schema: "Identity",
                table: "QuestionsAnswers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsAnswers_QuestionId",
                schema: "Identity",
                table: "QuestionsAnswers",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionsAnswers_Questions_QuestionId",
                schema: "Identity",
                table: "QuestionsAnswers",
                column: "QuestionId",
                principalSchema: "Identity",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionsAnswers_Questions_QuestionId",
                schema: "Identity",
                table: "QuestionsAnswers");

            migrationBuilder.DropIndex(
                name: "IX_QuestionsAnswers_QuestionId",
                schema: "Identity",
                table: "QuestionsAnswers");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                schema: "Identity",
                table: "QuestionsAnswers");

            migrationBuilder.AddColumn<long>(
                name: "CompetitionsQuestionsId",
                schema: "Identity",
                table: "QuestionsAnswers",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsAnswers_CompetitionsQuestionsId",
                schema: "Identity",
                table: "QuestionsAnswers",
                column: "CompetitionsQuestionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionsAnswers_Questions_CompetitionsQuestionsId",
                schema: "Identity",
                table: "QuestionsAnswers",
                column: "CompetitionsQuestionsId",
                principalSchema: "Identity",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
