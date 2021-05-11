using Microsoft.EntityFrameworkCore.Migrations;

namespace Health_Care.Migrations
{
    public partial class op : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "distnationUserId",
                table: "Appointment");

            migrationBuilder.AddColumn<string>(
                name: "appointmentStartFrom",
                table: "Appointment",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "appointmentUntilTo",
                table: "Appointment",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "distnationClinicId",
                table: "Appointment",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "appointmentStartFrom",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "appointmentUntilTo",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "distnationClinicId",
                table: "Appointment");

            migrationBuilder.AddColumn<int>(
                name: "distnationUserId",
                table: "Appointment",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
