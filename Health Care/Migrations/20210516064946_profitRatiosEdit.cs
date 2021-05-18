using Microsoft.EntityFrameworkCore.Migrations;

namespace Health_Care.Migrations
{
    public partial class profitRatiosEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "ProfitRatios");

            migrationBuilder.DropColumn(
                name: "name",
                table: "ProfitRatios");

            migrationBuilder.DropColumn(
                name: "percentage",
                table: "ProfitRatios");

            migrationBuilder.AddColumn<int>(
                name: "medicalExaminationPercentage",
                table: "ProfitRatios",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "servicePercentage",
                table: "ProfitRatios",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "medicalExaminationPercentage",
                table: "ProfitRatios");

            migrationBuilder.DropColumn(
                name: "servicePercentage",
                table: "ProfitRatios");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "ProfitRatios",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "ProfitRatios",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "percentage",
                table: "ProfitRatios",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
