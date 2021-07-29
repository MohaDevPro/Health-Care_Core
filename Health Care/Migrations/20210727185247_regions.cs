using Microsoft.EntityFrameworkCore.Migrations;

namespace Health_Care.Migrations
{
    public partial class regions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RegionId",
                table: "HealthcareWorkerRegions",
                newName: "RegionID");

            migrationBuilder.AlterColumn<int>(
                name: "RegionID",
                table: "HealthcareWorkerRegions",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_HealthcareWorkerRegions_RegionID",
                table: "HealthcareWorkerRegions",
                column: "RegionID");

            migrationBuilder.AddForeignKey(
                name: "FK_HealthcareWorkerRegions_Region_RegionID",
                table: "HealthcareWorkerRegions",
                column: "RegionID",
                principalTable: "Region",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthcareWorkerRegions_Region_RegionID",
                table: "HealthcareWorkerRegions");

            migrationBuilder.DropIndex(
                name: "IX_HealthcareWorkerRegions_RegionID",
                table: "HealthcareWorkerRegions");

            migrationBuilder.RenameColumn(
                name: "RegionID",
                table: "HealthcareWorkerRegions",
                newName: "RegionId");

            migrationBuilder.AlterColumn<int>(
                name: "RegionId",
                table: "HealthcareWorkerRegions",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
