using Microsoft.EntityFrameworkCore.Migrations;

namespace Health_Care.Migrations
{
    public partial class WorkrAppoitment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "acceptedAppointment",
                table: "WorkerAppointment");

            migrationBuilder.DropColumn(
                name: "appointmentId",
                table: "WorkerAppointment");

            migrationBuilder.AddColumn<bool>(
                name: "AcceptedByHealthWorker",
                table: "WorkerAppointment",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ConfirmHealthWorkerCome_ByHimself",
                table: "WorkerAppointment",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ConfirmHealthWorkerCome_ByPatient",
                table: "WorkerAppointment",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PercentageFromAppointmentPriceForApp",
                table: "WorkerAppointment",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "appointmentDate",
                table: "WorkerAppointment",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "appointmentShift",
                table: "WorkerAppointment",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "cancelReasonWrittenByHealthWorker",
                table: "WorkerAppointment",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "cancelledByHealthWorker",
                table: "WorkerAppointment",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "patientId",
                table: "WorkerAppointment",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "reservedAmountUntilConfirm",
                table: "WorkerAppointment",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "servicePrice",
                table: "WorkerAppointment",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcceptedByHealthWorker",
                table: "WorkerAppointment");

            migrationBuilder.DropColumn(
                name: "ConfirmHealthWorkerCome_ByHimself",
                table: "WorkerAppointment");

            migrationBuilder.DropColumn(
                name: "ConfirmHealthWorkerCome_ByPatient",
                table: "WorkerAppointment");

            migrationBuilder.DropColumn(
                name: "PercentageFromAppointmentPriceForApp",
                table: "WorkerAppointment");

            migrationBuilder.DropColumn(
                name: "appointmentDate",
                table: "WorkerAppointment");

            migrationBuilder.DropColumn(
                name: "appointmentShift",
                table: "WorkerAppointment");

            migrationBuilder.DropColumn(
                name: "cancelReasonWrittenByHealthWorker",
                table: "WorkerAppointment");

            migrationBuilder.DropColumn(
                name: "cancelledByHealthWorker",
                table: "WorkerAppointment");

            migrationBuilder.DropColumn(
                name: "patientId",
                table: "WorkerAppointment");

            migrationBuilder.DropColumn(
                name: "reservedAmountUntilConfirm",
                table: "WorkerAppointment");

            migrationBuilder.DropColumn(
                name: "servicePrice",
                table: "WorkerAppointment");

            migrationBuilder.AddColumn<bool>(
                name: "acceptedAppointment",
                table: "WorkerAppointment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "appointmentId",
                table: "WorkerAppointment",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
