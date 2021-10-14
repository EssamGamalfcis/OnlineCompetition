using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineCompetition.MVC.Migrations
{
    public partial class CompetitionQuestionsAnswers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionsAnswers",
                schema: "Identity");

            migrationBuilder.CreateTable(
                name: "CompetitionQuestionsAnswers",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAR = table.Column<string>(nullable: false),
                    NameEN = table.Column<string>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    CompetitionsId = table.Column<long>(nullable: false),
                    QuestionId = table.Column<long>(nullable: false),
                    AnswersMasterId = table.Column<long>(nullable: false),
                    AnswersDetailsId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionQuestionsAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetitionQuestionsAnswers_AnswersMaster_AnswersMasterId",
                        column: x => x.AnswersMasterId,
                        principalSchema: "Identity",
                        principalTable: "AnswersMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompetitionQuestionsAnswers_Competitions_CompetitionsId",
                        column: x => x.CompetitionsId,
                        principalSchema: "Identity",
                        principalTable: "Competitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompetitionQuestionsAnswers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalSchema: "Identity",
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionQuestionsAnswers_AnswersMasterId",
                schema: "Identity",
                table: "CompetitionQuestionsAnswers",
                column: "AnswersMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionQuestionsAnswers_CompetitionsId",
                schema: "Identity",
                table: "CompetitionQuestionsAnswers",
                column: "CompetitionsId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionQuestionsAnswers_QuestionId",
                schema: "Identity",
                table: "CompetitionQuestionsAnswers",
                column: "QuestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompetitionQuestionsAnswers",
                schema: "Identity");

            migrationBuilder.CreateTable(
                name: "QuestionsAnswers",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnswersDetailsId = table.Column<long>(type: "bigint", nullable: true),
                    AnswersMasterId = table.Column<long>(type: "bigint", nullable: true),
                    CompetitionsId = table.Column<long>(type: "bigint", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    NameAR = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameEN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestionId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionsAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionsAnswers_AnswersMaster_AnswersMasterId",
                        column: x => x.AnswersMasterId,
                        principalSchema: "Identity",
                        principalTable: "AnswersMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionsAnswers_Competitions_CompetitionsId",
                        column: x => x.CompetitionsId,
                        principalSchema: "Identity",
                        principalTable: "Competitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionsAnswers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalSchema: "Identity",
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsAnswers_AnswersMasterId",
                schema: "Identity",
                table: "QuestionsAnswers",
                column: "AnswersMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsAnswers_CompetitionsId",
                schema: "Identity",
                table: "QuestionsAnswers",
                column: "CompetitionsId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsAnswers_QuestionId",
                schema: "Identity",
                table: "QuestionsAnswers",
                column: "QuestionId");
        }
    }
}
