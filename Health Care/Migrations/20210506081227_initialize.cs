using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Health_Care.Migrations
{
    public partial class initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Appointment",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        userId = table.Column<int>(nullable: false),
            //        distnationUserId = table.Column<int>(nullable: false),
            //        appointmentTime = table.Column<string>(nullable: true),
            //        appointmentDate = table.Column<string>(nullable: true),
            //        Paid = table.Column<bool>(nullable: false),
            //        UserComeToAppointment = table.Column<bool>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Appointment", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "AppWorktime",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        userId = table.Column<int>(nullable: false),
            //        shiftAM_PM = table.Column<string>(nullable: false),
            //        day = table.Column<int>(nullable: false),
            //        startTime = table.Column<int>(nullable: false),
            //        endTime = table.Column<int>(nullable: false),
            //        RealOpenTime = table.Column<string>(nullable: true),
            //        RealClossTime = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AppWorktime", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Blogs",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        title = table.Column<string>(nullable: true),
            //        description = table.Column<string>(nullable: true),
            //        type = table.Column<string>(nullable: true),
            //        content = table.Column<string>(nullable: true),
            //        image = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Blogs", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ChargeOrRechargeRequest",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        userId = table.Column<int>(nullable: false),
            //        BalanceReceipt = table.Column<string>(nullable: true),
            //        rechargeDate = table.Column<string>(nullable: true),
            //        ConfirmToAddBalance = table.Column<bool>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ChargeOrRechargeRequest", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "clinicDoctors",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Clinicid = table.Column<int>(nullable: false),
            //        Doctorid = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_clinicDoctors", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ClinicType",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        ClinicTypeName = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ClinicType", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Contract",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        contractFor = table.Column<int>(nullable: false),
            //        contractPath = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Contract", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Conversation",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        userIdFrom = table.Column<int>(nullable: false),
            //        userIdTo = table.Column<int>(nullable: false),
            //        appointmentId = table.Column<int>(nullable: false),
            //        message = table.Column<string>(nullable: true),
            //        isRecived = table.Column<bool>(nullable: false),
            //        isReaded = table.Column<bool>(nullable: false),
            //        messageDate = table.Column<string>(nullable: true),
            //        messageTime = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Conversation", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Doctor",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Userid = table.Column<int>(nullable: false),
            //        name = table.Column<string>(nullable: true),
            //        Pictue = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Doctor", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ExternalClinic",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(nullable: true),
            //        Picture = table.Column<string>(nullable: true),
            //        ClinicTypeId = table.Column<int>(nullable: false),
            //        userId = table.Column<int>(nullable: false),
            //        doctorId = table.Column<int>(nullable: false),
            //        appointmentPrice = table.Column<int>(nullable: false),
            //        numberOfAvailableAppointment = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ExternalClinic", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ExternalClinicAppointment",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        appointmentId = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ExternalClinicAppointment", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "FCMTokens",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        UserID = table.Column<int>(nullable: false),
            //        DeviceID = table.Column<string>(nullable: true),
            //        Token = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_FCMTokens", x => x.ID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "HealthcareWorker",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        userId = table.Column<int>(nullable: false),
            //        Name = table.Column<string>(nullable: true),
            //        Picture = table.Column<string>(nullable: true),
            //        BackGroundPicture = table.Column<string>(nullable: true),
            //        Description = table.Column<string>(nullable: true),
            //        identificationImage = table.Column<string>(nullable: true),
            //        specialityId = table.Column<int>(nullable: false),
            //        graduationCertificateImage = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_HealthcareWorker", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "HealthcareWorkerWorkPlace",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        userId = table.Column<int>(nullable: false),
            //        workePlaceName = table.Column<string>(nullable: true),
            //        shiftAM_PM = table.Column<string>(nullable: false),
            //        startTime = table.Column<string>(nullable: true),
            //        endTime = table.Column<string>(nullable: true),
            //        Day = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_HealthcareWorkerWorkPlace", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "HospitalAppointment",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        appointmentId = table.Column<int>(nullable: false),
            //        clinicId = table.Column<int>(nullable: false),
            //        doctorid = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_HospitalAppointment", x => x.id);
            //    });

            migrationBuilder.CreateTable(
                name: "HospitalClinic",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(nullable: true),
                    picture = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    hospitalId = table.Column<int>(nullable: false),
                    clinicId = table.Column<int>(nullable: false),
                    appointmentPrice = table.Column<int>(nullable: false),
                    numberOfAvailableAppointment = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HospitalClinic", x => x.id);
                });

            //migrationBuilder.CreateTable(
            //    name: "HospitalClinicDoctor",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        hospitalClinicId = table.Column<int>(nullable: false),
            //        doctorId = table.Column<int>(nullable: false),
            //        startTime = table.Column<string>(nullable: true),
            //        endTime = table.Column<string>(nullable: true),
            //        Day = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_HospitalClinicDoctor", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "MedicalAdvice",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        title = table.Column<string>(nullable: true),
            //        paragraph = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_MedicalAdvice", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Notifications",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        title = table.Column<string>(nullable: true),
            //        body = table.Column<string>(nullable: true),
            //        time = table.Column<string>(nullable: true),
            //        isRepeated = table.Column<bool>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Notifications", x => x.ID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Patient",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        userId = table.Column<int>(nullable: false),
            //        gender = table.Column<string>(nullable: false),
            //        birthDate = table.Column<string>(nullable: true),
            //        chronicDiseases = table.Column<string>(nullable: true),
            //        Balance = table.Column<int>(nullable: false),
            //        LastBalanceChargeDate = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Patient", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ProfitFromTheApp",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        userId = table.Column<int>(nullable: false),
            //        balance = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ProfitFromTheApp", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Region",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        regionName = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Region", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ReportAndComplaint",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        complaintTypeid = table.Column<int>(nullable: false),
            //        name = table.Column<string>(nullable: true),
            //        description = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ReportAndComplaint", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Role",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        RoleName = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Role", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Service",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        serviceName = table.Column<string>(nullable: true),
            //        servicePrice = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Service", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Speciality",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        specialityName = table.Column<string>(nullable: true),
            //        isBasic = table.Column<bool>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Speciality", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "SpeciallyDoctors",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Doctorid = table.Column<int>(nullable: false),
            //        Specialityid = table.Column<int>(nullable: false),
            //        Roleid = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_SpeciallyDoctors", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "User",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        nameAR = table.Column<string>(nullable: true),
            //        nameEN = table.Column<string>(nullable: true),
            //        phoneNumber = table.Column<string>(nullable: true),
            //        address = table.Column<string>(nullable: true),
            //        regionId = table.Column<int>(nullable: false),
            //        email = table.Column<string>(nullable: true),
            //        Password = table.Column<string>(nullable: true),
            //        DeviceId = table.Column<string>(nullable: true),
            //        isActiveAccount = table.Column<bool>(nullable: false),
            //        Roleid = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_User", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "UserContract",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        userId = table.Column<int>(nullable: false),
            //        contractId = table.Column<int>(nullable: false),
            //        contractStartDate = table.Column<string>(nullable: true),
            //        contractEndDate = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UserContract", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "WorkerAppointment",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        appointmentId = table.Column<int>(nullable: false),
            //        serviceId = table.Column<int>(nullable: false),
            //        workerId = table.Column<int>(nullable: false),
            //        regionId = table.Column<int>(nullable: false),
            //        acceptedAppointment = table.Column<bool>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_WorkerAppointment", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "WorkerSalary",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        workerId = table.Column<int>(nullable: false),
            //        salary = table.Column<int>(nullable: false),
            //        isPaid = table.Column<bool>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_WorkerSalary", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "HealthcareWorkerService",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        userId = table.Column<int>(nullable: false),
            //        serviceId = table.Column<int>(nullable: false),
            //        HealthcareWorkerid = table.Column<int>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_HealthcareWorkerService", x => x.id);
            //        table.ForeignKey(
            //            name: "FK_HealthcareWorkerService_HealthcareWorker_HealthcareWorkerid",
            //            column: x => x.HealthcareWorkerid,
            //            principalTable: "HealthcareWorker",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_HealthcareWorkerService_Service_serviceId",
            //            column: x => x.serviceId,
            //            principalTable: "Service",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "RefreshTokens",
            //    columns: table => new
            //    {
            //        TokenId = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        UserId = table.Column<int>(nullable: false),
            //        Token = table.Column<string>(nullable: true),
            //        ExpiryDate = table.Column<DateTime>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_RefreshTokens", x => x.TokenId);
            //        table.ForeignKey(
            //            name: "FK_RefreshTokens_User_UserId",
            //            column: x => x.UserId,
            //            principalTable: "User",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_HealthcareWorkerService_HealthcareWorkerid",
            //    table: "HealthcareWorkerService",
            //    column: "HealthcareWorkerid");

            //migrationBuilder.CreateIndex(
            //    name: "IX_HealthcareWorkerService_serviceId",
            //    table: "HealthcareWorkerService",
            //    column: "serviceId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_RefreshTokens_UserId",
            //    table: "RefreshTokens",
            //    column: "UserId",
            //    unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "Appointment");

            //migrationBuilder.DropTable(
            //    name: "AppWorktime");

            //migrationBuilder.DropTable(
            //    name: "Blogs");

            //migrationBuilder.DropTable(
            //    name: "ChargeOrRechargeRequest");

            //migrationBuilder.DropTable(
            //    name: "clinicDoctors");

            //migrationBuilder.DropTable(
            //    name: "ClinicType");

            //migrationBuilder.DropTable(
            //    name: "Contract");

            //migrationBuilder.DropTable(
            //    name: "Conversation");

            //migrationBuilder.DropTable(
            //    name: "Doctor");

            //migrationBuilder.DropTable(
            //    name: "ExternalClinic");

            //migrationBuilder.DropTable(
            //    name: "ExternalClinicAppointment");

            //migrationBuilder.DropTable(
            //    name: "FCMTokens");

            //migrationBuilder.DropTable(
            //    name: "HealthcareWorkerService");

            //migrationBuilder.DropTable(
            //    name: "HealthcareWorkerWorkPlace");

            //migrationBuilder.DropTable(
            //    name: "HospitalAppointment");

            //migrationBuilder.DropTable(
            //    name: "HospitalClinic");

            //migrationBuilder.DropTable(
            //    name: "HospitalClinicDoctor");

            //migrationBuilder.DropTable(
            //    name: "MedicalAdvice");

            //migrationBuilder.DropTable(
            //    name: "Notifications");

            //migrationBuilder.DropTable(
            //    name: "Patient");

            //migrationBuilder.DropTable(
            //    name: "ProfitFromTheApp");

            //migrationBuilder.DropTable(
            //    name: "RefreshTokens");

            //migrationBuilder.DropTable(
            //    name: "Region");

            //migrationBuilder.DropTable(
            //    name: "ReportAndComplaint");

            //migrationBuilder.DropTable(
            //    name: "Role");

            //migrationBuilder.DropTable(
            //    name: "Speciality");

            //migrationBuilder.DropTable(
            //    name: "SpeciallyDoctors");

            //migrationBuilder.DropTable(
            //    name: "UserContract");

            //migrationBuilder.DropTable(
            //    name: "WorkerAppointment");

            //migrationBuilder.DropTable(
            //    name: "WorkerSalary");

            //migrationBuilder.DropTable(
            //    name: "HealthcareWorker");

            //migrationBuilder.DropTable(
            //    name: "Service");

            //migrationBuilder.DropTable(
            //    name: "User");
        }
    }
}
