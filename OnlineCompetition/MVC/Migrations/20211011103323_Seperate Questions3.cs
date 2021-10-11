using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineCompetition.MVC.Migrations
{
    public partial class SeperateQuestions3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionsAnswers_CompetitionsQuestions_CompetitionsQuestionsId",
                schema: "Identity",
                table: "QuestionsAnswers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompetitionsQuestions",
                schema: "Identity",
                table: "CompetitionsQuestions");

            migrationBuilder.RenameTable(
                name: "CompetitionsQuestions",
                schema: "Identity",
                newName: "Questions",
                newSchema: "Identity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Questions",
                schema: "Identity",
                table: "Questions",
                column: "Id");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionsAnswers_Questions_CompetitionsQuestionsId",
                schema: "Identity",
                table: "QuestionsAnswers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Questions",
                schema: "Identity",
                table: "Questions");

            migrationBuilder.RenameTable(
                name: "Questions",
                schema: "Identity",
                newName: "CompetitionsQuestions",
                newSchema: "Identity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompetitionsQuestions",
                schema: "Identity",
                table: "CompetitionsQuestions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionsAnswers_CompetitionsQuestions_CompetitionsQuestionsId",
                schema: "Identity",
                table: "QuestionsAnswers",
                column: "CompetitionsQuestionsId",
                principalSchema: "Identity",
                principalTable: "CompetitionsQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
