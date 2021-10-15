using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineCompetition.MVC.Migrations
{
    public partial class SolveCompetition1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "StudentScore",
                schema: "Identity",
                table: "StudentCompetitionQuestionAnswer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentScore",
                schema: "Identity",
                table: "StudentCompetitionQuestionAnswer");
        }
    }
}
