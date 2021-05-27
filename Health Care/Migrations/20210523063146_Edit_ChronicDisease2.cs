using Microsoft.EntityFrameworkCore.Migrations;

namespace Health_Care.Migrations
{
    public partial class Edit_ChronicDisease2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Patientid",
                table: "ChronicDiseases",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChronicDiseases_Patientid",
                table: "ChronicDiseases",
                column: "Patientid");

            migrationBuilder.AddForeignKey(
                name: "FK_ChronicDiseases_Patient_Patientid",
                table: "ChronicDiseases",
                column: "Patientid",
                principalTable: "Patient",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChronicDiseases_Patient_Patientid",
                table: "ChronicDiseases");

            migrationBuilder.DropIndex(
                name: "IX_ChronicDiseases_Patientid",
                table: "ChronicDiseases");

            migrationBuilder.DropColumn(
                name: "Patientid",
                table: "ChronicDiseases");
        }
    }
}
