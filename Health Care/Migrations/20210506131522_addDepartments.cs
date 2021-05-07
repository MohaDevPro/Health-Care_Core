using Microsoft.EntityFrameworkCore.Migrations;

namespace Health_Care.Migrations
{
    public partial class addDepartments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "appointmentPrice",
                table: "HospitalClinic");

            migrationBuilder.DropColumn(
                name: "clinicId",
                table: "HospitalClinic");

            migrationBuilder.DropColumn(
                name: "numberOfAvailableAppointment",
                table: "HospitalClinic");

            migrationBuilder.DropColumn(
                name: "appointmentPrice",
                table: "ExternalClinic");

            migrationBuilder.DropColumn(
                name: "numberOfAvailableAppointment",
                table: "ExternalClinic");

            migrationBuilder.DropColumn(
                name: "Pictue",
                table: "Doctor");

            migrationBuilder.AddColumn<int>(
                name: "HospitalDepartmentsID",
                table: "ExternalClinic",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "Doctor",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "appointmentPrice",
                table: "Doctor",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "numberOfAvailableAppointment",
                table: "Doctor",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "departmentsOfHospitals",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Picture = table.Column<string>(nullable: true),
                    Background = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_departmentsOfHospitals", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "hospitalDepartments",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepatmentsOfHospitalID = table.Column<int>(nullable: false),
                    HospitalClinicid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hospitalDepartments", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "departmentsOfHospitals");

            migrationBuilder.DropTable(
                name: "hospitalDepartments");

            migrationBuilder.DropColumn(
                name: "HospitalDepartmentsID",
                table: "ExternalClinic");

            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "appointmentPrice",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "numberOfAvailableAppointment",
                table: "Doctor");

            migrationBuilder.AddColumn<int>(
                name: "appointmentPrice",
                table: "HospitalClinic",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "clinicId",
                table: "HospitalClinic",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "numberOfAvailableAppointment",
                table: "HospitalClinic",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "appointmentPrice",
                table: "ExternalClinic",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "numberOfAvailableAppointment",
                table: "ExternalClinic",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Pictue",
                table: "Doctor",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
