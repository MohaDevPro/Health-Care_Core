using Microsoft.EntityFrameworkCore.Migrations;

namespace Health_Care.Migrations
{
    public partial class firstEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "detailedSpecialityId",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "specialityId",
                table: "Doctor");

            migrationBuilder.AddColumn<bool>(
                name: "isBasic",
                table: "Speciality",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "SpeciallyDoctors",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Doctorid = table.Column<int>(nullable: false),
                    Specialityid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpeciallyDoctors", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpeciallyDoctors");

            migrationBuilder.DropColumn(
                name: "isBasic",
                table: "Speciality");

            migrationBuilder.AddColumn<string>(
                name: "detailedSpecialityId",
                table: "Doctor",
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
