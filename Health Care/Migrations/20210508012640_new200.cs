using Microsoft.EntityFrameworkCore.Migrations;

namespace Health_Care.Migrations
{
    public partial class new200 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthcareWorkerService_Service_serviceId",
                table: "HealthcareWorkerService");

            migrationBuilder.DropTable(
                name: "HospitalClinic");

            migrationBuilder.DropIndex(
                name: "IX_HealthcareWorkerService_serviceId",
                table: "HealthcareWorkerService");

            migrationBuilder.DropColumn(
                name: "servicePrice",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "HospitalClinicid",
                table: "hospitalDepartments");

            migrationBuilder.AddColumn<int>(
                name: "Hospitalid",
                table: "hospitalDepartments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Hospitals",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Picture = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    hospitalId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hospitals", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hospitals");

            migrationBuilder.DropColumn(
                name: "Hospitalid",
                table: "hospitalDepartments");

            migrationBuilder.AddColumn<int>(
                name: "servicePrice",
                table: "Service",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HospitalClinicid",
                table: "hospitalDepartments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "HospitalClinic",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    hospitalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HospitalClinic", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HealthcareWorkerService_serviceId",
                table: "HealthcareWorkerService",
                column: "serviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_HealthcareWorkerService_Service_serviceId",
                table: "HealthcareWorkerService",
                column: "serviceId",
                principalTable: "Service",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
