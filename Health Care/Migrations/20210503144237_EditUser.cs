using Microsoft.EntityFrameworkCore.Migrations;

namespace Health_Care.Migrations
{
    public partial class EditUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                table: "User");

            migrationBuilder.AddColumn<string>(
                name: "DeviceId",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "nameAR",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "nameEN",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isReaded",
                table: "Conversation",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isRecived",
                table: "Conversation",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "nameAR",
                table: "User");

            migrationBuilder.DropColumn(
                name: "nameEN",
                table: "User");

            migrationBuilder.DropColumn(
                name: "isReaded",
                table: "Conversation");

            migrationBuilder.DropColumn(
                name: "isRecived",
                table: "Conversation");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
