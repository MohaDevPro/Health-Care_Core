using Microsoft.EntityFrameworkCore.Migrations;

namespace Health_Care.Migrations
{
    public partial class edit2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HospitalID",
                table: "ExternalClinic");

            migrationBuilder.DropColumn(
                name: "IsOnHospital",
                table: "ExternalClinic");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HospitalID",
                table: "ExternalClinic",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsOnHospital",
                table: "ExternalClinic",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
