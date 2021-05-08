using Microsoft.EntityFrameworkCore.Migrations;

namespace Health_Care.Migrations
{
    public partial class editAppontmentModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserComeToAppointment",
                table: "Appointment");

            migrationBuilder.AddColumn<bool>(
                name: "PatientComeToAppointment",
                table: "ExternalClinicAppointment",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Accepted",
                table: "Appointment",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PatientComeToAppointment",
                table: "Appointment",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "TypeOfAppointment",
                table: "Appointment",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "doctorId",
                table: "Appointment",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PatientComeToAppointment",
                table: "ExternalClinicAppointment");

            migrationBuilder.DropColumn(
                name: "Accepted",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "PatientComeToAppointment",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "TypeOfAppointment",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "doctorId",
                table: "Appointment");

            migrationBuilder.AddColumn<bool>(
                name: "UserComeToAppointment",
                table: "Appointment",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
