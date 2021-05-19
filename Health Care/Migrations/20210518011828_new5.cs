using Microsoft.EntityFrameworkCore.Migrations;

namespace Health_Care.Migrations
{
    public partial class new5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BalanceReceipt",
                table: "ChargeOrRechargeRequest",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BalanceReceiptImage",
                table: "ChargeOrRechargeRequest",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BalanceReceiptImage",
                table: "ChargeOrRechargeRequest");

            migrationBuilder.AlterColumn<string>(
                name: "BalanceReceipt",
                table: "ChargeOrRechargeRequest",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
