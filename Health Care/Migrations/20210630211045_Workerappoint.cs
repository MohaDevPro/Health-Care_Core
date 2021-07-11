using Microsoft.EntityFrameworkCore.Migrations;

namespace Health_Care.Migrations
{
    public partial class Workerappoint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodeConfirmation",
                table: "WorkerAppointment",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodeConfirmation",
                table: "WorkerAppointment");
        }
    }
}
