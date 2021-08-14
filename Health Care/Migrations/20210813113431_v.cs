using Microsoft.EntityFrameworkCore.Migrations;

namespace Health_Care.Migrations
{
    public partial class v : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppointmentWorker",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    workerId = table.Column<int>(nullable: false),
                    appointmentDate = table.Column<string>(nullable: true),
                    numberOfAvailableAppointment = table.Column<int>(nullable: false),
                    numberOfRealAppointment = table.Column<int>(nullable: false),
                    totalProfitFromRealAppointment = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentWorker", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentWorker");
        }
    }
}
