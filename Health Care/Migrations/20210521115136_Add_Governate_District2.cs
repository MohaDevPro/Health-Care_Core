using Microsoft.EntityFrameworkCore.Migrations;

namespace Health_Care.Migrations
{
    public partial class Add_Governate_District2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "regionName",
                table: "Region");

            migrationBuilder.AddColumn<int>(
                name: "DistrictID",
                table: "Region",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Region",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GovernorateID",
                table: "District",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DistrictID",
                table: "Region");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Region");

            migrationBuilder.DropColumn(
                name: "GovernorateID",
                table: "District");

            migrationBuilder.AddColumn<string>(
                name: "regionName",
                table: "Region",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
