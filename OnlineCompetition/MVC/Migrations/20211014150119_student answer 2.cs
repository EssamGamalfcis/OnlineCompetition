using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineCompetition.MVC.Migrations
{
    public partial class studentanswer2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentCompetitionQuestionAnswer_AnswersMaster_AnswersMasterId",
                schema: "Identity",
                table: "StudentCompetitionQuestionAnswer");

            migrationBuilder.DropColumn(
                name: "AnswersDetailId",
                schema: "Identity",
                table: "StudentCompetitionQuestionAnswer");


            migrationBuilder.DropColumn(
                name: "AnswersText",
                schema: "Identity",
                table: "StudentCompetitionQuestionAnswer");

            migrationBuilder.AlterColumn<long>(
                name: "AnswersMasterId",
                schema: "Identity",
                table: "StudentCompetitionQuestionAnswer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ActualAnswersDetailId",
                schema: "Identity",
                table: "StudentCompetitionQuestionAnswer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ActualAnswersText",
                schema: "Identity",
                table: "StudentCompetitionQuestionAnswer",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RightAnswersDetailsId",
                schema: "Identity",
                table: "StudentCompetitionQuestionAnswer",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCompetitionQuestionAnswer_AnswersMaster_AnswersMasterId",
                schema: "Identity",
                table: "StudentCompetitionQuestionAnswer",
                column: "AnswersMasterId",
                principalSchema: "Identity",
                principalTable: "AnswersMaster",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentCompetitionQuestionAnswer_AnswersMaster_AnswersMasterId",
                schema: "Identity",
                table: "StudentCompetitionQuestionAnswer");

            migrationBuilder.DropColumn(
                name: "ActualAnswersDetailId",
                schema: "Identity",
                table: "StudentCompetitionQuestionAnswer");

            migrationBuilder.DropColumn(
                name: "ActualAnswersText",
                schema: "Identity",
                table: "StudentCompetitionQuestionAnswer");

            migrationBuilder.DropColumn(
                name: "RightAnswersDetailsId",
                schema: "Identity",
                table: "StudentCompetitionQuestionAnswer");

            migrationBuilder.AlterColumn<long>(
                name: "AnswersMasterId",
                schema: "Identity",
                table: "StudentCompetitionQuestionAnswer",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<long>(
                name: "AnswersDetailId",
                schema: "Identity",
                table: "StudentCompetitionQuestionAnswer",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AnswersText",
                schema: "Identity",
                table: "StudentCompetitionQuestionAnswer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCompetitionQuestionAnswer_AnswersMaster_AnswersMasterId",
                schema: "Identity",
                table: "StudentCompetitionQuestionAnswer",
                column: "AnswersMasterId",
                principalSchema: "Identity",
                principalTable: "AnswersMaster",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
