using Microsoft.EntityFrameworkCore.Migrations;

namespace Health_Care.Migrations
{
    public partial class new8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "PercentageFromAppointmentPriceForApp",
                table: "Appointment",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "PercentageFromAppointmentPriceForApp",
                table: "Appointment",
                type: "bit",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
