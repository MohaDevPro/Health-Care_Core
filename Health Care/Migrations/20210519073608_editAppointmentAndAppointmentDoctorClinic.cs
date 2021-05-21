using Microsoft.EntityFrameworkCore.Migrations;

namespace Health_Care.Migrations
{
    public partial class editAppointmentAndAppointmentDoctorClinic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "totalProfitFromRealAppointment",
                table: "AppointmentDoctorClinic",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "appointmentDoctorClinicId",
                table: "Appointment",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "totalProfitFromRealAppointment",
                table: "AppointmentDoctorClinic");

            migrationBuilder.DropColumn(
                name: "appointmentDoctorClinicId",
                table: "Appointment");
        }
    }
}
