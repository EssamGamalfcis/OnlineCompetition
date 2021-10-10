using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineCompetition.MVC.Migrations
{
    public partial class Test3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                                    name: "TotalGrade",
                                    schema: "Identity",
                                    table: "CompetitionsQuestions",
                                    type: "real",
                                    nullable: false,
                                    defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                                    name: "TotalGrade",
                                    schema: "Identity",
                                    table: "CompetitionsQuestions");
        }
    }
}
