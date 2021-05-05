using Microsoft.EntityFrameworkCore.Migrations;

namespace Health_Care.Migrations
{
    public partial class hj : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "Description",
            //    table: "HospitalClinic");

            //migrationBuilder.DropColumn(
            //    name: "Name",
            //    table: "HospitalClinic");

            //migrationBuilder.DropColumn(
            //    name: "Picture",
            //    table: "HospitalClinic");

            migrationBuilder.AlterColumn<string>(
                name: "ClinicTypeName",
                table: "ClinicType",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<string>(
            //    name: "Description",
            //    table: "HospitalClinic",
            //    type: "nvarchar(max)",
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "Name",
            //    table: "HospitalClinic",
            //    type: "nvarchar(max)",
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "Picture",
            //    table: "HospitalClinic",
            //    type: "nvarchar(max)",
            //    nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClinicTypeName",
                table: "ClinicType",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
