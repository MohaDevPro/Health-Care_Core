using Microsoft.EntityFrameworkCore.Migrations;

namespace Health_Care.Migrations
{
    public partial class reportsandcomplains : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "complaintTypeid",
                table: "ReportAndComplaint");

            migrationBuilder.DropColumn(
                name: "name",
                table: "ReportAndComplaint");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "ReportAndComplaint",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "isAnswered",
                table: "ReportAndComplaint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "replyByAdmin",
                table: "ReportAndComplaint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "title",
                table: "ReportAndComplaint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "ReportAndComplaint",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "PercentageFromAppointmentPriceForApp",
                table: "Appointment",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "ReportAndComplaint");

            migrationBuilder.DropColumn(
                name: "isAnswered",
                table: "ReportAndComplaint");

            migrationBuilder.DropColumn(
                name: "replyByAdmin",
                table: "ReportAndComplaint");

            migrationBuilder.DropColumn(
                name: "title",
                table: "ReportAndComplaint");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "ReportAndComplaint");

            migrationBuilder.AddColumn<int>(
                name: "complaintTypeid",
                table: "ReportAndComplaint",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "ReportAndComplaint",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "PercentageFromAppointmentPriceForApp",
                table: "Appointment",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
