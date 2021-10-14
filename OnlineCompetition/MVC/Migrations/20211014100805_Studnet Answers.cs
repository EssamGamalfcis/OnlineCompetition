using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineCompetition.MVC.Migrations
{
    public partial class StudnetAnswers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentCompetitionQuestionAnswer",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentUserId = table.Column<string>(nullable: true),
                    CompetitionId = table.Column<long>(nullable: false),
                    QuestionsId = table.Column<long>(nullable: false),
                    AnswersMasterId = table.Column<long>(nullable: false),
                    AnswersDetailId = table.Column<long>(nullable: true),
                    AnswersText = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCompetitionQuestionAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentCompetitionQuestionAnswer_AnswersMaster_AnswersMasterId",
                        column: x => x.AnswersMasterId,
                        principalSchema: "Identity",
                        principalTable: "AnswersMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentCompetitionQuestionAnswer_Competitions_CompetitionId",
                        column: x => x.CompetitionId,
                        principalSchema: "Identity",
                        principalTable: "Competitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentCompetitionQuestionAnswer_Questions_QuestionsId",
                        column: x => x.QuestionsId,
                        principalSchema: "Identity",
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });


            migrationBuilder.CreateIndex(
                name: "IX_StudentCompetitionQuestionAnswer_AnswersMasterId",
                schema: "Identity",
                table: "StudentCompetitionQuestionAnswer",
                column: "AnswersMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCompetitionQuestionAnswer_CompetitionId",
                schema: "Identity",
                table: "StudentCompetitionQuestionAnswer",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCompetitionQuestionAnswer_QuestionsId",
                schema: "Identity",
                table: "StudentCompetitionQuestionAnswer",
                column: "QuestionsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentCompetitionQuestionAnswer",
                schema: "Identity");
        }
    }
}
