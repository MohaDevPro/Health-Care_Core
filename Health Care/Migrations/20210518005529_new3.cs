using Microsoft.EntityFrameworkCore.Migrations;

namespace Health_Care.Migrations
{
<<<<<<< HEAD:Health Care/Migrations/20210522104010_Edit_HealthcareWorkerRegions2.cs
    public partial class Edit_HealthcareWorkerRegions2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Userid",
                table: "HealthcareWorkerRegions");
=======
    public partial class new3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "clinicId",
                table: "AppWorktime",
                nullable: false,
                defaultValue: 0);
>>>>>>> origin/master:Health Care/Migrations/20210518005529_new3.cs
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
<<<<<<< HEAD:Health Care/Migrations/20210522104010_Edit_HealthcareWorkerRegions2.cs
            migrationBuilder.AddColumn<int>(
                name: "Userid",
                table: "HealthcareWorkerRegions",
                type: "int",
                nullable: false,
                defaultValue: 0);
=======
            migrationBuilder.DropColumn(
                name: "clinicId",
                table: "AppWorktime");
>>>>>>> origin/master:Health Care/Migrations/20210518005529_new3.cs
        }
    }
}
