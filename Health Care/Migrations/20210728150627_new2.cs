using Microsoft.EntityFrameworkCore.Migrations;

namespace Health_Care.Migrations
{
    public partial class new2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "shiftAM_PM",
                table: "HealthCareWorkerAppWorkTime",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1)");

            migrationBuilder.AddColumn<bool>(
                name: "IsAdditional",
                table: "HealthCareWorkerAppWorkTime",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdditional",
                table: "HealthCareWorkerAppWorkTime");

            migrationBuilder.AlterColumn<string>(
                name: "shiftAM_PM",
                table: "HealthCareWorkerAppWorkTime",
                type: "nvarchar(1)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
