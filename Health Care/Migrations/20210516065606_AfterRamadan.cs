using Microsoft.EntityFrameworkCore.Migrations;

namespace Health_Care.Migrations
{
    public partial class AfterRamadan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BackgoundImage",
                table: "Hospitals",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BackgoundImage",
                table: "ExternalClinic",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "backgroundImage",
                table: "Doctor",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackgoundImage",
                table: "Hospitals");

            migrationBuilder.DropColumn(
                name: "BackgoundImage",
                table: "ExternalClinic");

            migrationBuilder.DropColumn(
                name: "backgroundImage",
                table: "Doctor");
        }
    }
}
