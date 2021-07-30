using Microsoft.EntityFrameworkCore.Migrations;

namespace Health_Care.Migrations
{
    public partial class edit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "clinicId",
                table: "AppWorktime");

            migrationBuilder.AddColumn<int>(
                name: "ExternalClinicId",
                table: "AppWorktime",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AppWorktime_ExternalClinicId",
                table: "AppWorktime",
                column: "ExternalClinicId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppWorktime_ExternalClinic_ExternalClinicId",
                table: "AppWorktime",
                column: "ExternalClinicId",
                principalTable: "ExternalClinic",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppWorktime_ExternalClinic_ExternalClinicId",
                table: "AppWorktime");

            migrationBuilder.DropIndex(
                name: "IX_AppWorktime_ExternalClinicId",
                table: "AppWorktime");

            migrationBuilder.DropColumn(
                name: "ExternalClinicId",
                table: "AppWorktime");

            migrationBuilder.AddColumn<int>(
                name: "clinicId",
                table: "AppWorktime",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
