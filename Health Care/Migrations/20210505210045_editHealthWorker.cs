using Microsoft.EntityFrameworkCore.Migrations;

namespace Health_Care.Migrations
{
    public partial class editHealthWorker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {




            migrationBuilder.AddColumn<string>(
                name: "BackGroundPicture",
                table: "HealthcareWorker",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "HealthcareWorker",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "HealthcareWorker",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "HealthcareWorker",
                nullable: true);


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicalAdvice");

            migrationBuilder.DropColumn(
                name: "BackGroundPicture",
                table: "HealthcareWorker");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "HealthcareWorker");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "HealthcareWorker");

            migrationBuilder.DropColumn(
                name: "Picture",
                table: "HealthcareWorker");

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
