using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineCompetition.MVC.Migrations
{
    public partial class linkstudentwithcompetition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompetitionsUsers_User_StudentUserId",
                schema: "Identity",
                table: "CompetitionsUsers");

            migrationBuilder.DropIndex(
                name: "IX_CompetitionsUsers_StudentUserId",
                schema: "Identity",
                table: "CompetitionsUsers");

            migrationBuilder.AlterColumn<long>(
                name: "StudentUserId",
                schema: "Identity",
                table: "CompetitionsUsers",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "SolvedBefore",
                schema: "Identity",
                table: "CompetitionsUsers",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<float>(
                name: "Score",
                schema: "Identity",
                table: "CompetitionsUsers",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<string>(
                name: "StudentUserId1",
                schema: "Identity",
                table: "CompetitionsUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionsUsers_StudentUserId1",
                schema: "Identity",
                table: "CompetitionsUsers",
                column: "StudentUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CompetitionsUsers_User_StudentUserId1",
                schema: "Identity",
                table: "CompetitionsUsers",
                column: "StudentUserId1",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompetitionsUsers_User_StudentUserId1",
                schema: "Identity",
                table: "CompetitionsUsers");

            migrationBuilder.DropIndex(
                name: "IX_CompetitionsUsers_StudentUserId1",
                schema: "Identity",
                table: "CompetitionsUsers");

            migrationBuilder.DropColumn(
                name: "StudentUserId1",
                schema: "Identity",
                table: "CompetitionsUsers");

            migrationBuilder.AlterColumn<string>(
                name: "StudentUserId",
                schema: "Identity",
                table: "CompetitionsUsers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<bool>(
                name: "SolvedBefore",
                schema: "Identity",
                table: "CompetitionsUsers",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Score",
                schema: "Identity",
                table: "CompetitionsUsers",
                type: "real",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionsUsers_StudentUserId",
                schema: "Identity",
                table: "CompetitionsUsers",
                column: "StudentUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompetitionsUsers_User_StudentUserId",
                schema: "Identity",
                table: "CompetitionsUsers",
                column: "StudentUserId",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
