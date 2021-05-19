using Microsoft.EntityFrameworkCore.Migrations;

namespace Health_Care.Migrations
{
    public partial class editAppointmentProfitfromAppModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "balance",
                table: "ProfitFromTheApp");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "ProfitFromTheApp");

            migrationBuilder.AddColumn<int>(
                name: "clinicId",
                table: "ProfitFromTheApp",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "doctorId",
                table: "ProfitFromTheApp",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "hospitalId",
                table: "ProfitFromTheApp",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "profit",
                table: "ProfitFromTheApp",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "sumOfAppointment",
                table: "ProfitFromTheApp",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "PercentageFromAppointmentPriceForApp",
                table: "Appointment",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "clinicId",
                table: "ProfitFromTheApp");

            migrationBuilder.DropColumn(
                name: "doctorId",
                table: "ProfitFromTheApp");

            migrationBuilder.DropColumn(
                name: "hospitalId",
                table: "ProfitFromTheApp");

            migrationBuilder.DropColumn(
                name: "profit",
                table: "ProfitFromTheApp");

            migrationBuilder.DropColumn(
                name: "sumOfAppointment",
                table: "ProfitFromTheApp");

            migrationBuilder.DropColumn(
                name: "PercentageFromAppointmentPriceForApp",
                table: "Appointment");

            migrationBuilder.AddColumn<int>(
                name: "balance",
                table: "ProfitFromTheApp",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "ProfitFromTheApp",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
