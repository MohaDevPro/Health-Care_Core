using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Health_Care.Migrations
{
    public partial class ff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.CreateTable(
                name: "HealthCareWorkerAppWorkTime",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(nullable: false),
                    shiftAM_PM = table.Column<string>(nullable: false),
                    day = table.Column<int>(nullable: false),
                    startTime = table.Column<int>(nullable: false),
                    endTime = table.Column<int>(nullable: false),
                    RealOpenTime = table.Column<string>(nullable: true),
                    RealClossTime = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthCareWorkerAppWorkTime", x => x.id);
                });

        }

    }
}
