using Microsoft.EntityFrameworkCore.Migrations;

namespace Health_Care.Migrations
{
    public partial class EditDoctor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "graduationCertificateImage",
                table: "Doctor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "identificationImage",
                table: "Doctor",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "graduationCertificateImage",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "identificationImage",
                table: "Doctor");
        }
    }
}
