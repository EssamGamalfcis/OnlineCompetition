using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineCompetition.MVC.Migrations
{
    public partial class answers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompetitionsQuestions_Competitions_CompetitionId",
                schema: "Identity",
                table: "CompetitionsQuestions");

            migrationBuilder.DropTable(
                name: "CompetitionsQuestionsAnswers",
                schema: "Identity");

            migrationBuilder.DropColumn(
                name: "AnswerType",
                schema: "Identity",
                table: "CompetitionsQuestions");


            migrationBuilder.AlterColumn<long>(
                name: "CompetitionId",
                schema: "Identity",
                table: "CompetitionsQuestions",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateTable(
                name: "AnswersMaster",
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
                    AnswerType = table.Column<int>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswersMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnswersDetails",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnswerMasterId = table.Column<long>(nullable: true),
                    AnswerText = table.Column<string>(nullable: true)
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
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAR = table.Column<string>(nullable: false),
                    NameEN = table.Column<string>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    CompetitionsQuestionsId = table.Column<long>(nullable: true),
                    AnswersMasterId = table.Column<long>(nullable: true),
                    AnswersDetailsId = table.Column<long>(nullable: true)
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
                        name: "FK_QuestionsAnswers_CompetitionsQuestions_CompetitionsQuestionsId",
                        column: x => x.CompetitionsQuestionsId,
                        principalSchema: "Identity",
                        principalTable: "CompetitionsQuestions",
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
                name: "IX_QuestionsAnswers_CompetitionsQuestionsId",
                schema: "Identity",
                table: "QuestionsAnswers",
                column: "CompetitionsQuestionsId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompetitionsQuestions_Competitions_CompetitionId",
                schema: "Identity",
                table: "CompetitionsQuestions");

            migrationBuilder.DropTable(
                name: "AnswersDetails",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "QuestionsAnswers",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "AnswersMaster",
                schema: "Identity");

            migrationBuilder.AlterColumn<long>(
                name: "CompetitionId",
                schema: "Identity",
                table: "CompetitionsQuestions",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AnswerType",
                schema: "Identity",
                table: "CompetitionsQuestions",
                type: "int",
                nullable: false,
                defaultValue: 0);


            migrationBuilder.CreateTable(
                name: "CompetitionsQuestionsAnswers",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompetitionsQuestionsId = table.Column<long>(type: "bigint", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    NameAR = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameEN = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionsQuestionsAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetitionsQuestionsAnswers_CompetitionsQuestions_CompetitionsQuestionsId",
                        column: x => x.CompetitionsQuestionsId,
                        principalSchema: "Identity",
                        principalTable: "CompetitionsQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionsQuestionsAnswers_CompetitionsQuestionsId",
                schema: "Identity",
                table: "CompetitionsQuestionsAnswers",
                column: "CompetitionsQuestionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompetitionsQuestions_Competitions_CompetitionId",
                schema: "Identity",
                table: "CompetitionsQuestions",
                column: "CompetitionId",
                principalSchema: "Identity",
                principalTable: "Competitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
