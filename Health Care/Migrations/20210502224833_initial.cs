using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Health_Care.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Userid",
                table: "Doctor",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "HealthcareWorker",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(nullable: false),
                    identificationImage = table.Column<byte[]>(nullable: true),
                    specialityId = table.Column<int>(nullable: false),
                    graduationCertificateImage = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthcareWorker", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HealthcareWorker");

            migrationBuilder.DropColumn(
                name: "Userid",
                table: "Doctor");
        }
    }
}
