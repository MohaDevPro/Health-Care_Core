using Microsoft.EntityFrameworkCore.Migrations;

namespace Health_Care.Migrations
{
    public partial class IsRestore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "note",
                table: "Contract",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRestore",
                table: "ChargeOrRechargeRequest",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "note",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "IsRestore",
                table: "ChargeOrRechargeRequest");
        }
    }
}
