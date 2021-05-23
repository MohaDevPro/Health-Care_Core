using Microsoft.EntityFrameworkCore.Migrations;

namespace Health_Care.Migrations
{
    public partial class Add_Governate_District : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "specialityId",
                table: "HealthcareWorker");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "HealthcareWorker",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReagionID",
                table: "HealthcareWorker",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "WorkPlace",
                table: "HealthcareWorker",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "speciality",
                table: "HealthcareWorker",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "District",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_District", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Governorate",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Governorate", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "District");

            migrationBuilder.DropTable(
                name: "Governorate");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "HealthcareWorker");

            migrationBuilder.DropColumn(
                name: "ReagionID",
                table: "HealthcareWorker");

            migrationBuilder.DropColumn(
                name: "WorkPlace",
                table: "HealthcareWorker");

            migrationBuilder.DropColumn(
                name: "speciality",
                table: "HealthcareWorker");

            migrationBuilder.AddColumn<int>(
                name: "specialityId",
                table: "HealthcareWorker",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
