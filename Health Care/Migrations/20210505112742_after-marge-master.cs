using Microsoft.EntityFrameworkCore.Migrations;

namespace Health_Care.Migrations
{
    public partial class aftermargemaster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "endTime",
                table: "ExternalClinic");

            migrationBuilder.DropColumn(
                name: "startTime",
                table: "ExternalClinic");

            migrationBuilder.DropColumn(
                name: "specialityId",
                table: "Doctor");

            migrationBuilder.CreateTable(
                name: "MedicalAdvice",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(nullable: true),
                    paragraph = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalAdvice", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicalAdvice");

            migrationBuilder.AddColumn<string>(
                name: "endTime",
                table: "ExternalClinic",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "startTime",
                table: "ExternalClinic",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "specialityId",
                table: "Doctor",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
