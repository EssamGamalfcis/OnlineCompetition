using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineCompetition.MVC.Migrations
{
    public partial class ContinueMakingTheDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Competitions",
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
                    Duration = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Questionnaire",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAR = table.Column<string>(nullable: false),
                    NameEN = table.Column<string>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questionnaire", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompetitionsQuestions",
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
                    CompetitionId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionsQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetitionsQuestions_Competitions_CompetitionId",
                        column: x => x.CompetitionId,
                        principalSchema: "Identity",
                        principalTable: "Competitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompetitionsUsers",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentUserId1 = table.Column<string>(nullable: true),
                    StudentUserId = table.Column<Guid>(nullable: false),
                    CompetitionId = table.Column<long>(nullable: false),
                    Score = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionsUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetitionsUsers_Competitions_CompetitionId",
                        column: x => x.CompetitionId,
                        principalSchema: "Identity",
                        principalTable: "Competitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompetitionsUsers_User_StudentUserId1",
                        column: x => x.StudentUserId1,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionnaireAnswers",
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
                    QuestionnaireId = table.Column<long>(nullable: false),
                    AnswerType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionnaireAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionnaireAnswers_Questionnaire_QuestionnaireId",
                        column: x => x.QuestionnaireId,
                        principalSchema: "Identity",
                        principalTable: "Questionnaire",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionnaireCategory",
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
                    QuestionnaireId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionnaireCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionnaireCategory_Questionnaire_QuestionnaireId",
                        column: x => x.QuestionnaireId,
                        principalSchema: "Identity",
                        principalTable: "Questionnaire",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionnaireUsers",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId1 = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    QuestionnaireId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionnaireUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionnaireUsers_Questionnaire_QuestionnaireId",
                        column: x => x.QuestionnaireId,
                        principalSchema: "Identity",
                        principalTable: "Questionnaire",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionnaireUsers_User_UserId1",
                        column: x => x.UserId1,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompetitionsQuestionsAnswers",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompetitionsQuestionsId = table.Column<long>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "QuestionnaireCategoryElements",
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
                    QuestionnaireCategoryId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionnaireCategoryElements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionnaireCategoryElements_QuestionnaireCategory_QuestionnaireCategoryId",
                        column: x => x.QuestionnaireCategoryId,
                        principalSchema: "Identity",
                        principalTable: "QuestionnaireCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionsQuestions_CompetitionId",
                schema: "Identity",
                table: "CompetitionsQuestions",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionsQuestionsAnswers_CompetitionsQuestionsId",
                schema: "Identity",
                table: "CompetitionsQuestionsAnswers",
                column: "CompetitionsQuestionsId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionsUsers_CompetitionId",
                schema: "Identity",
                table: "CompetitionsUsers",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionsUsers_StudentUserId1",
                schema: "Identity",
                table: "CompetitionsUsers",
                column: "StudentUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireAnswers_QuestionnaireId",
                schema: "Identity",
                table: "QuestionnaireAnswers",
                column: "QuestionnaireId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireCategory_QuestionnaireId",
                schema: "Identity",
                table: "QuestionnaireCategory",
                column: "QuestionnaireId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireCategoryElements_QuestionnaireCategoryId",
                schema: "Identity",
                table: "QuestionnaireCategoryElements",
                column: "QuestionnaireCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireUsers_QuestionnaireId",
                schema: "Identity",
                table: "QuestionnaireUsers",
                column: "QuestionnaireId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireUsers_UserId1",
                schema: "Identity",
                table: "QuestionnaireUsers",
                column: "UserId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompetitionsQuestionsAnswers",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "CompetitionsUsers",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "QuestionnaireAnswers",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "QuestionnaireCategoryElements",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "QuestionnaireUsers",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "CompetitionsQuestions",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "QuestionnaireCategory",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Competitions",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Questionnaire",
                schema: "Identity");
        }
    }
}
