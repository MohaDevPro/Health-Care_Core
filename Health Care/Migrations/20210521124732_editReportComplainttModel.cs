using Microsoft.EntityFrameworkCore.Migrations;

namespace Health_Care.Migrations
{
    public partial class editReportComplainttModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "replyByAdmin",
                table: "ReportAndComplaint");

            migrationBuilder.AddColumn<string>(
                name: "ReportAndComplaintDate",
                table: "ReportAndComplaint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReportAndComplaintTime",
                table: "ReportAndComplaint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "replyTextByAdmin",
                table: "ReportAndComplaint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReportAndComplaintDate",
                table: "ReportAndComplaint");

            migrationBuilder.DropColumn(
                name: "ReportAndComplaintTime",
                table: "ReportAndComplaint");

            migrationBuilder.DropColumn(
                name: "replyTextByAdmin",
                table: "ReportAndComplaint");

            migrationBuilder.AddColumn<string>(
                name: "replyByAdmin",
                table: "ReportAndComplaint",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
