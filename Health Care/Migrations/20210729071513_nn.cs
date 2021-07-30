using Microsoft.EntityFrameworkCore.Migrations;

namespace Health_Care.Migrations
{
    public partial class nn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "DoctorClinicReqeusts",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromID = table.Column<int>(nullable: false),
                    ToID = table.Column<int>(nullable: false),
                    IsAccepted = table.Column<bool>(nullable: false),
                    IsCanceled = table.Column<bool>(nullable: false),
                    CancelResoun = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorClinicReqeusts", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorClinicReqeusts");

            migrationBuilder.DropColumn(
                name: "IsAdditional",
                table: "HealthCareWorkerAppWorkTime");
        }
    }
}
