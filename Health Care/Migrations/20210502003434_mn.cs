using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Health_Care.Migrations
{
    public partial class mn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Appointment",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(nullable: false),
                    distnationUserId = table.Column<int>(nullable: false),
                    appointmentTime = table.Column<string>(nullable: true),
                    appointmentDate = table.Column<string>(nullable: true),
                    Paid = table.Column<bool>(nullable: false),
                    UserComeToAppointment = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointment", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AppWorktime",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(nullable: false),
                    shiftAM_PM = table.Column<string>(nullable: false),
                    day = table.Column<string>(nullable: true),
                    startTime = table.Column<string>(nullable: true),
                    endTime = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppWorktime", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ChargeOrRechargeRequest",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(nullable: false),
                    BalanceReceipt = table.Column<byte[]>(nullable: true),
                    rechargeDate = table.Column<string>(nullable: true),
                    ConfirmToAddBalance = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChargeOrRechargeRequest", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ClinicType",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClinicTypeName = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicType", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    contractFor = table.Column<int>(nullable: false),
                    contractPath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Conversation",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userIdFrom = table.Column<int>(nullable: false),
                    userIdTo = table.Column<int>(nullable: false),
                    appointmentId = table.Column<int>(nullable: false),
                    message = table.Column<string>(nullable: true),
                    messageDate = table.Column<string>(nullable: true),
                    messageTime = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversation", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(nullable: true),
                    specialityId = table.Column<int>(nullable: false),
                    detailedSpecialityId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ExternalClinic",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClinicTypeId = table.Column<int>(nullable: false),
                    userId = table.Column<int>(nullable: false),
                    doctorId = table.Column<int>(nullable: false),
                    startTime = table.Column<string>(nullable: true),
                    endTime = table.Column<string>(nullable: true),
                    appointmentPrice = table.Column<int>(nullable: false),
                    numberOfAvailableAppointment = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalClinic", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ExternalClinicAppointment",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    appointmentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalClinicAppointment", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "HealthcareWorkerService",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(nullable: false),
                    serviceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthcareWorkerService", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "HealthcareWorkerWorkPlace",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(nullable: false),
                    workePlaceName = table.Column<string>(nullable: true),
                    shiftAM_PM = table.Column<string>(nullable: false),
                    startTime = table.Column<string>(nullable: true),
                    endTime = table.Column<string>(nullable: true),
                    Day = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthcareWorkerWorkPlace", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "HospitalAppointment",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    appointmentId = table.Column<int>(nullable: false),
                    clinicId = table.Column<int>(nullable: false),
                    doctorid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HospitalAppointment", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "HospitalClinic",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    hospitalId = table.Column<int>(nullable: false),
                    clinicId = table.Column<int>(nullable: false),
                    appointmentPrice = table.Column<int>(nullable: false),
                    numberOfAvailableAppointment = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HospitalClinic", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "HospitalClinicDoctor",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    hospitalClinicId = table.Column<int>(nullable: false),
                    doctorId = table.Column<int>(nullable: false),
                    startTime = table.Column<string>(nullable: true),
                    endTime = table.Column<string>(nullable: true),
                    Day = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HospitalClinicDoctor", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(nullable: false),
                    gender = table.Column<string>(nullable: false),
                    birthDate = table.Column<string>(nullable: true),
                    chronicDiseases = table.Column<string>(nullable: true),
                    Balance = table.Column<int>(nullable: false),
                    LastBalanceChargeDate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ProfitFromTheApp",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(nullable: false),
                    balance = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfitFromTheApp", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Region",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    regionName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ReportAndComplaint",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    complaintTypeid = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportAndComplaint", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    serviceName = table.Column<string>(nullable: true),
                    servicePrice = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Speciality",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    specialityName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speciality", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(nullable: true),
                    phoneNumber = table.Column<string>(nullable: true),
                    address = table.Column<string>(nullable: true),
                    regionId = table.Column<int>(nullable: false),
                    email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    isActiveAccount = table.Column<bool>(nullable: false),
                    Roleid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "UserContract",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(nullable: false),
                    contractId = table.Column<int>(nullable: false),
                    contractStartDate = table.Column<string>(nullable: true),
                    contractEndDate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserContract", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "WorkerAppointment",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    appointmentId = table.Column<int>(nullable: false),
                    serviceId = table.Column<int>(nullable: false),
                    workerId = table.Column<int>(nullable: false),
                    regionId = table.Column<int>(nullable: false),
                    acceptedAppointment = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkerAppointment", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "WorkerSalary",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    workerId = table.Column<int>(nullable: false),
                    salary = table.Column<int>(nullable: false),
                    isPaid = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkerSalary", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointment");

            migrationBuilder.DropTable(
                name: "AppWorktime");

            migrationBuilder.DropTable(
                name: "ChargeOrRechargeRequest");

            migrationBuilder.DropTable(
                name: "ClinicType");

            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.DropTable(
                name: "Conversation");

            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.DropTable(
                name: "ExternalClinic");

            migrationBuilder.DropTable(
                name: "ExternalClinicAppointment");

            migrationBuilder.DropTable(
                name: "HealthcareWorkerService");

            migrationBuilder.DropTable(
                name: "HealthcareWorkerWorkPlace");

            migrationBuilder.DropTable(
                name: "HospitalAppointment");

            migrationBuilder.DropTable(
                name: "HospitalClinic");

            migrationBuilder.DropTable(
                name: "HospitalClinicDoctor");

            migrationBuilder.DropTable(
                name: "Patient");

            migrationBuilder.DropTable(
                name: "ProfitFromTheApp");

            migrationBuilder.DropTable(
                name: "Region");

            migrationBuilder.DropTable(
                name: "ReportAndComplaint");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropTable(
                name: "Speciality");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "UserContract");

            migrationBuilder.DropTable(
                name: "WorkerAppointment");

            migrationBuilder.DropTable(
                name: "WorkerSalary");
        }
    }
}
