using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineCompetition.MVC.Migrations
{
    public partial class linkstudentwithcompetition2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "StudentUserId",
                schema: "Identity",
                table: "CompetitionsUsers",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentUserId1",
                schema: "Identity",
                table: "CompetitionsUsers",
                type: "nvarchar(450)",
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
    }
}
