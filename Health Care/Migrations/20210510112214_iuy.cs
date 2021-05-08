using Microsoft.EntityFrameworkCore.Migrations;

namespace Health_Care.Migrations
{
    public partial class iuy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "numberOfAvailableAppointment",
                table: "ExternalClinic",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "numberOfAvailableAppointment",
                table: "ExternalClinic");
        }
    }
}
