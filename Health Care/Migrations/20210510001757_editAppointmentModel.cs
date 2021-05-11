using Microsoft.EntityFrameworkCore.Migrations;

namespace Health_Care.Migrations
{
    public partial class editAppointmentModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "appointmentForUserHimself",
                table: "Appointment",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "patientName",
                table: "Appointment",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "patientphone",
                table: "Appointment",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "visitReason",
                table: "Appointment",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "appointmentForUserHimself",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "patientName",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "patientphone",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "visitReason",
                table: "Appointment");
        }
    }
}
