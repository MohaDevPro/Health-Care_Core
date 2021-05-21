using Microsoft.EntityFrameworkCore.Migrations;

namespace Health_Care.Migrations
{
    public partial class Edit_HealthcareWorkerSpeciality : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "speciality",
                table: "HealthcareWorker");

            migrationBuilder.AddColumn<int>(
                name: "specialityID",
                table: "HealthcareWorker",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "specialityID",
                table: "HealthcareWorker");

            migrationBuilder.AddColumn<string>(
                name: "speciality",
                table: "HealthcareWorker",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
