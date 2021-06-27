using Microsoft.EntityFrameworkCore.Migrations;

namespace Health_Care.Migrations
{
    public partial class EditHospital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "hospitalId",
                table: "Hospitals");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Hospitals",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Hospitals");

            migrationBuilder.AddColumn<int>(
                name: "hospitalId",
                table: "Hospitals",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
