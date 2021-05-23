using Microsoft.EntityFrameworkCore.Migrations;

namespace Health_Care.Migrations
{
    public partial class Edit_HealthcareWorkerRegions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HealthcareWorkerRegions",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Userid = table.Column<int>(nullable: false),
                    RegionId = table.Column<int>(nullable: false),
                    HealthcareWorkerid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthcareWorkerRegions", x => x.id);
                    table.ForeignKey(
                        name: "FK_HealthcareWorkerRegions_HealthcareWorker_HealthcareWorkerid",
                        column: x => x.HealthcareWorkerid,
                        principalTable: "HealthcareWorker",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HealthcareWorkerRegions_HealthcareWorkerid",
                table: "HealthcareWorkerRegions",
                column: "HealthcareWorkerid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HealthcareWorkerRegions");
        }
    }
}
