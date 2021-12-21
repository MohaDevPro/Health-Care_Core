using Microsoft.EntityFrameworkCore.Migrations;

namespace Health_Care.Migrations
{
    public partial class edit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfitFromTheApp");

            migrationBuilder.DropTable(
                name: "WorkerSalary");

            //migrationBuilder.AddColumn<bool>(
            //    name: "IsRestored",
            //    table: "ChargeOrRechargeRequest",
            //    nullable: false,
            //    defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "numberofAvailableAppointment",
                table: "AppWorktime",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRestored",
                table: "ChargeOrRechargeRequest");

            migrationBuilder.DropColumn(
                name: "numberofAvailableAppointment",
                table: "AppWorktime");

            migrationBuilder.CreateTable(
                name: "ProfitFromTheApp",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    clinicId = table.Column<int>(type: "int", nullable: false),
                    doctorId = table.Column<int>(type: "int", nullable: false),
                    hospitalId = table.Column<int>(type: "int", nullable: false),
                    profit = table.Column<int>(type: "int", nullable: false),
                    sumOfAppointment = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfitFromTheApp", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "WorkerSalary",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    isPaid = table.Column<bool>(type: "bit", nullable: false),
                    salary = table.Column<int>(type: "int", nullable: false),
                    workerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkerSalary", x => x.id);
                });
        }
    }
}
