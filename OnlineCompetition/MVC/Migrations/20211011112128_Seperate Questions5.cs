using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineCompetition.MVC.Migrations
{
    public partial class SeperateQuestions5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnswersDetails",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "QuestionsAnswers",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "AnswersMaster",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Questions",
                schema: "Identity");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnswersMaster",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnswerType = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    NameAR = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameEN = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswersMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    NameAR = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameEN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    TotalScore = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnswersDetails",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnswerMasterId = table.Column<long>(type: "bigint", nullable: true),
                    AnswerText = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswersDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnswersDetails_AnswersMaster_AnswerMasterId",
                        column: x => x.AnswerMasterId,
                        principalSchema: "Identity",
                        principalTable: "AnswersMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "IX_AnswersDetails_AnswerMasterId",
                schema: "Identity",
                table: "AnswersDetails",
                column: "AnswerMasterId");

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
