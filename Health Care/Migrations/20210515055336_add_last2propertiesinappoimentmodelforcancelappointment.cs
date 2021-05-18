using Microsoft.EntityFrameworkCore.Migrations;

namespace Health_Care.Migrations
{
    public partial class add_last2propertiesinappoimentmodelforcancelappointment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Background",
                table: "departmentsOfHospitals");

            migrationBuilder.DropColumn(
                name: "Picture",
                table: "departmentsOfHospitals");

            migrationBuilder.AddColumn<string>(
                name: "Background",
                table: "hospitalDepartments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "hospitalDepartments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "cancelReasonWrittenBySecretary",
                table: "Appointment",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "cancelledByClinicSecretary",
                table: "Appointment",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Background",
                table: "hospitalDepartments");

            migrationBuilder.DropColumn(
                name: "Picture",
                table: "hospitalDepartments");

            migrationBuilder.DropColumn(
                name: "cancelReasonWrittenBySecretary",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "cancelledByClinicSecretary",
                table: "Appointment");

            migrationBuilder.AddColumn<string>(
                name: "Background",
                table: "departmentsOfHospitals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "departmentsOfHospitals",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
