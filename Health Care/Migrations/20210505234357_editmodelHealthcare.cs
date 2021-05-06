using Microsoft.EntityFrameworkCore.Migrations;

namespace Health_Care.Migrations
{
    public partial class editmodelHealthcare : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthcareWorkerService_HealthcareWorker_HealthcareWorkerid",
                table: "HealthcareWorkerService");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "HealthcareWorkerService");

            migrationBuilder.AlterColumn<int>(
                name: "HealthcareWorkerid",
                table: "HealthcareWorkerService",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HealthcareWorkerService_HealthcareWorker_HealthcareWorkerid",
                table: "HealthcareWorkerService",
                column: "HealthcareWorkerid",
                principalTable: "HealthcareWorker",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthcareWorkerService_HealthcareWorker_HealthcareWorkerid",
                table: "HealthcareWorkerService");

            migrationBuilder.AlterColumn<int>(
                name: "HealthcareWorkerid",
                table: "HealthcareWorkerService",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "HealthcareWorkerService",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_HealthcareWorkerService_HealthcareWorker_HealthcareWorkerid",
                table: "HealthcareWorkerService",
                column: "HealthcareWorkerid",
                principalTable: "HealthcareWorker",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
