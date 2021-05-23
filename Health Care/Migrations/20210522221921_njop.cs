using Microsoft.EntityFrameworkCore.Migrations;

namespace Health_Care.Migrations
{
    public partial class njop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthcareWorkerService_HealthcareWorker_HealthcareWorkerid",
                table: "HealthcareWorkerService");

            //migrationBuilder.DropColumn(
            //    name: "Price",
            //    table: "HealthcareWorkerService");

            migrationBuilder.DropColumn(
                name: "serviceId",
                table: "HealthcareWorkerService");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "HealthcareWorkerService");

            migrationBuilder.AlterColumn<int>(
                name: "HealthcareWorkerid",
                table: "HealthcareWorkerService",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "HealthcareWorkeridd",
                table: "HealthcareWorkerService",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Pricee",
                table: "HealthcareWorkerService",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "serviceIdd",
                table: "HealthcareWorkerService",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "userIdd",
                table: "HealthcareWorkerService",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_HealthcareWorkerService_HealthcareWorker_HealthcareWorkerid",
            //    table: "HealthcareWorkerService");

            //migrationBuilder.DropColumn(
            //    name: "HealthcareWorkeridd",
            //    table: "HealthcareWorkerService");

            //migrationBuilder.DropColumn(
            //    name: "Pricee",
            //    table: "HealthcareWorkerService");

            //migrationBuilder.DropColumn(
            //    name: "serviceIdd",
            //    table: "HealthcareWorkerService");

            //migrationBuilder.DropColumn(
            //    name: "userIdd",
            //    table: "HealthcareWorkerService");

            migrationBuilder.AlterColumn<int>(
                name: "HealthcareWorkerid",
                table: "HealthcareWorkerService",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "HealthcareWorkerService",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "serviceId",
                table: "HealthcareWorkerService",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                onDelete: ReferentialAction.Cascade);
        }
    }
}
