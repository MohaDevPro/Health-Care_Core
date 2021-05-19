using Microsoft.EntityFrameworkCore.Migrations;

namespace Health_Care.Migrations
{
    public partial class new2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClinicID",
                table: "AppWorktime");

            migrationBuilder.AddColumn<bool>(
                name: "IsCanceled",
                table: "ChargeOrRechargeRequest",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "NumberOfReceipt",
                table: "ChargeOrRechargeRequest",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCanceled",
                table: "ChargeOrRechargeRequest");

            migrationBuilder.DropColumn(
                name: "NumberOfReceipt",
                table: "ChargeOrRechargeRequest");

            migrationBuilder.AddColumn<int>(
                name: "ClinicID",
                table: "AppWorktime",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
