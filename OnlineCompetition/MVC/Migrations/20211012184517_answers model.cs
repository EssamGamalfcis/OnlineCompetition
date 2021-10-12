using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineCompetition.MVC.Migrations
{
    public partial class answersmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswersDetails_AnswersMaster_AnswerMasterId",
                schema: "Identity",
                table: "AnswersDetails");


            migrationBuilder.AlterColumn<long>(
                name: "AnswerMasterId",
                schema: "Identity",
                table: "AnswersDetails",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AnswersDetails_AnswersMaster_AnswerMasterId",
                schema: "Identity",
                table: "AnswersDetails",
                column: "AnswerMasterId",
                principalSchema: "Identity",
                principalTable: "AnswersMaster",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswersDetails_AnswersMaster_AnswerMasterId",
                schema: "Identity",
                table: "AnswersDetails");

            migrationBuilder.AlterColumn<long>(
                name: "AnswerMasterId",
                schema: "Identity",
                table: "AnswersDetails",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_AnswersDetails_AnswersMaster_AnswerMasterId",
                schema: "Identity",
                table: "AnswersDetails",
                column: "AnswerMasterId",
                principalSchema: "Identity",
                principalTable: "AnswersMaster",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
