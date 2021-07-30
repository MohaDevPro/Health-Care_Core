using Microsoft.EntityFrameworkCore.Migrations;

namespace Health_Care.Migrations
{
    public partial class regions2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthcareWorkerRegions_HealthcareWorker_HealthcareWorkerid",
                table: "HealthcareWorkerRegions");

            migrationBuilder.DropForeignKey(
                name: "FK_HealthcareWorkerRegions_Region_RegionID",
                table: "HealthcareWorkerRegions");

            migrationBuilder.AlterColumn<int>(
                name: "RegionID",
                table: "HealthcareWorkerRegions",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HealthcareWorkerid",
                table: "HealthcareWorkerRegions",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HealthcareWorkerRegions_HealthcareWorker_HealthcareWorkerid",
                table: "HealthcareWorkerRegions",
                column: "HealthcareWorkerid",
                principalTable: "HealthcareWorker",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HealthcareWorkerRegions_Region_RegionID",
                table: "HealthcareWorkerRegions",
                column: "RegionID",
                principalTable: "Region",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthcareWorkerRegions_HealthcareWorker_HealthcareWorkerid",
                table: "HealthcareWorkerRegions");

            migrationBuilder.DropForeignKey(
                name: "FK_HealthcareWorkerRegions_Region_RegionID",
                table: "HealthcareWorkerRegions");

            migrationBuilder.AlterColumn<int>(
                name: "RegionID",
                table: "HealthcareWorkerRegions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "HealthcareWorkerid",
                table: "HealthcareWorkerRegions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_HealthcareWorkerRegions_HealthcareWorker_HealthcareWorkerid",
                table: "HealthcareWorkerRegions",
                column: "HealthcareWorkerid",
                principalTable: "HealthcareWorker",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HealthcareWorkerRegions_Region_RegionID",
                table: "HealthcareWorkerRegions",
                column: "RegionID",
                principalTable: "Region",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
