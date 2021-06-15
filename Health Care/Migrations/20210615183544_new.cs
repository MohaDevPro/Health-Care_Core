using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Health_Care.Migrations
{
    public partial class @new : Migration
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
                    distnationClinicId = table.Column<int>(nullable: false),
                    doctorId = table.Column<int>(nullable: false),
                    appointmentPrice = table.Column<int>(nullable: false),
                    appointmentDate = table.Column<string>(nullable: true),
                    TypeOfAppointment = table.Column<string>(nullable: true),
                    appointmentForUserHimself = table.Column<bool>(nullable: false),
                    appointmentStartFrom = table.Column<string>(nullable: true),
                    appointmentUntilTo = table.Column<string>(nullable: true),
                    patientName = table.Column<string>(nullable: true),
                    patientphone = table.Column<string>(nullable: true),
                    visitReason = table.Column<string>(nullable: true),
                    PatientComeToAppointment = table.Column<bool>(nullable: false),
                    Paid = table.Column<bool>(nullable: false),
                    PercentageFromAppointmentPriceForApp = table.Column<int>(nullable: false),
                    Accepted = table.Column<bool>(nullable: false),
                    cancelledByUser = table.Column<bool>(nullable: false),
                    cancelledByClinicSecretary = table.Column<bool>(nullable: false),
                    cancelReasonWrittenBySecretary = table.Column<string>(nullable: true),
                    appointmentDoctorClinicId = table.Column<string>(nullable: true),
                    active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointment", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AppointmentDoctorClinic",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    clinicId = table.Column<int>(nullable: false),
                    doctorId = table.Column<int>(nullable: false),
                    appointmentDate = table.Column<string>(nullable: true),
                    numberOfAvailableAppointment = table.Column<int>(nullable: false),
                    numberOfRealAppointment = table.Column<int>(nullable: false),
                    totalProfitFromRealAppointment = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentDoctorClinic", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AppWorktime",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(nullable: false),
                    clinicId = table.Column<int>(nullable: false),
                    shiftAM_PM = table.Column<string>(nullable: true),
                    day = table.Column<int>(nullable: false),
                    startTime = table.Column<int>(nullable: false),
                    endTime = table.Column<int>(nullable: false),
                    RealOpenTime = table.Column<string>(nullable: true),
                    RealClossTime = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppWorktime", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    type = table.Column<string>(nullable: true),
                    content = table.Column<string>(nullable: true),
                    image = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ChargeOrRechargeRequest",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(nullable: false),
                    NumberOfReceipt = table.Column<string>(nullable: true),
                    BalanceReceiptImage = table.Column<string>(nullable: true),
                    BalanceReceipt = table.Column<int>(nullable: false),
                    rechargeDate = table.Column<string>(nullable: true),
                    ConfirmToAddBalance = table.Column<bool>(nullable: false),
                    IsCanceled = table.Column<bool>(nullable: false),
                    ResounOfCancel = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChargeOrRechargeRequest", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "clinicDoctors",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clinicid = table.Column<int>(nullable: false),
                    Doctorid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clinicDoctors", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ClinicType",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClinicTypeName = table.Column<string>(nullable: true),
                    active = table.Column<bool>(nullable: false)
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
                    active = table.Column<bool>(nullable: false)
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
                    isRecived = table.Column<bool>(nullable: false),
                    isReaded = table.Column<bool>(nullable: false),
                    messageDate = table.Column<string>(nullable: true),
                    messageTime = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversation", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "departmentsOfHospitals",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_departmentsOfHospitals", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "District",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GovernorateID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_District", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Userid = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    Picture = table.Column<string>(nullable: true),
                    backgroundImage = table.Column<string>(nullable: true),
                    appointmentPrice = table.Column<int>(nullable: false),
                    numberOfAvailableAppointment = table.Column<int>(nullable: false),
                    identificationImage = table.Column<string>(nullable: true),
                    graduationCertificateImage = table.Column<string>(nullable: true),
                    active = table.Column<bool>(nullable: false)
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
                    Name = table.Column<string>(nullable: true),
                    Picture = table.Column<string>(nullable: true),
                    BackgoundImage = table.Column<string>(nullable: true),
                    ClinicTypeId = table.Column<int>(nullable: false),
                    userId = table.Column<int>(nullable: false),
                    doctorId = table.Column<int>(nullable: false),
                    numberOfAvailableAppointment = table.Column<int>(nullable: false),
                    HospitalDepartmentsID = table.Column<int>(nullable: false),
                    active = table.Column<bool>(nullable: false)
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
                    appointmentId = table.Column<int>(nullable: false),
                    PatientComeToAppointment = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalClinicAppointment", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Favorite",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<string>(nullable: true),
                    PatientId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorite", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "FCMTokens",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(nullable: false),
                    DeviceID = table.Column<string>(nullable: true),
                    Token = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FCMTokens", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Governorate",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Governorate", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "HealthcareWorker",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(nullable: false),
                    ReagionID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    WorkPlace = table.Column<string>(nullable: true),
                    specialityId = table.Column<int>(nullable: false),
                    Picture = table.Column<string>(nullable: true),
                    BackGroundPicture = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    identificationImage = table.Column<string>(nullable: true),
                    graduationCertificateImage = table.Column<string>(nullable: true),
                    active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthcareWorker", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "HealthCareWorkerAppWorkTime",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(nullable: false),
                    shiftAM_PM = table.Column<string>(nullable: false),
                    day = table.Column<int>(nullable: false),
                    startTime = table.Column<int>(nullable: false),
                    endTime = table.Column<int>(nullable: false),
                    RealOpenTime = table.Column<string>(nullable: true),
                    RealClossTime = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthCareWorkerAppWorkTime", x => x.id);
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
                name: "HealthWorkerRequestByUser",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    appointmentId = table.Column<int>(nullable: false),
                    RequestTime = table.Column<string>(nullable: true),
                    RequestDate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthWorkerRequestByUser", x => x.id);
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
                name: "HospitalClinicAddress",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    hospitalOrClinicId = table.Column<int>(nullable: false),
                    latitude = table.Column<float>(nullable: false),
                    longitude = table.Column<float>(nullable: false),
                    detailedAddress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HospitalClinicAddress", x => x.id);
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
                name: "Hospitals",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Picture = table.Column<string>(nullable: true),
                    BackgoundImage = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    active = table.Column<bool>(nullable: false),
                    hospitalId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hospitals", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "MedicalAdvice",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(nullable: true),
                    paragraph = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalAdvice", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(nullable: true),
                    body = table.Column<string>(nullable: true),
                    time = table.Column<string>(nullable: true),
                    isRepeated = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(nullable: false),
                    gender = table.Column<string>(nullable: true),
                    birthDate = table.Column<string>(nullable: true),
                    Balance = table.Column<int>(nullable: false),
                    LastBalanceChargeDate = table.Column<string>(nullable: true),
                    active = table.Column<bool>(nullable: false)
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
                    hospitalId = table.Column<int>(nullable: false),
                    clinicId = table.Column<int>(nullable: false),
                    doctorId = table.Column<int>(nullable: false),
                    sumOfAppointment = table.Column<int>(nullable: false),
                    profit = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfitFromTheApp", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ProfitRatios",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    medicalExaminationPercentage = table.Column<int>(nullable: false),
                    servicePercentage = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfitRatios", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Region",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistrictID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ReportAndComplaint",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: true),
                    userId = table.Column<int>(nullable: false),
                    title = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    replyTextByAdmin = table.Column<string>(nullable: true),
                    ReportAndComplaintDate = table.Column<string>(nullable: true),
                    ReportAndComplaintTime = table.Column<string>(nullable: true),
                    isAnswered = table.Column<bool>(nullable: false)
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
                    RoleName = table.Column<string>(nullable: true),
                    active = table.Column<bool>(nullable: false)
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
                    servicePrice = table.Column<int>(nullable: false),
                    active = table.Column<bool>(nullable: false)
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
                    specialityName = table.Column<string>(nullable: true),
                    BasicSpecialityID = table.Column<int>(nullable: false),
                    isBasic = table.Column<bool>(nullable: false),
                    active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speciality", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "SpecialityHealthWorker",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialityHealthWorker", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "SpeciallyDoctors",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Doctorid = table.Column<int>(nullable: false),
                    Specialityid = table.Column<int>(nullable: false),
                    Roleid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpeciallyDoctors", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nameAR = table.Column<string>(nullable: true),
                    nameEN = table.Column<string>(nullable: true),
                    phoneNumber = table.Column<string>(nullable: true),
                    address = table.Column<string>(nullable: true),
                    regionId = table.Column<int>(nullable: false),
                    email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    DeviceId = table.Column<string>(nullable: true),
                    isActiveAccount = table.Column<bool>(nullable: false),
                    Roleid = table.Column<int>(nullable: false),
                    completeData = table.Column<bool>(nullable: false),
                    active = table.Column<bool>(nullable: false)
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
                    serviceId = table.Column<int>(nullable: false),
                    workerId = table.Column<int>(nullable: false),
                    patientId = table.Column<int>(nullable: false),
                    regionId = table.Column<int>(nullable: false),
                    servicePrice = table.Column<int>(nullable: false),
                    appointmentDate = table.Column<string>(nullable: true),
                    appointmentShift = table.Column<string>(nullable: false),
                    ConfirmHealthWorkerCome_ByPatient = table.Column<bool>(nullable: false),
                    ConfirmHealthWorkerCome_ByHimself = table.Column<bool>(nullable: false),
                    reservedAmountUntilConfirm = table.Column<bool>(nullable: false),
                    PercentageFromAppointmentPriceForApp = table.Column<int>(nullable: false),
                    AcceptedByHealthWorker = table.Column<bool>(nullable: false),
                    cancelledByHealthWorker = table.Column<bool>(nullable: false),
                    cancelReasonWrittenByHealthWorker = table.Column<string>(nullable: true)
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

            migrationBuilder.CreateTable(
                name: "ContractTerms",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    contractID = table.Column<int>(nullable: false),
                    term = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractTerms", x => x.id);
                    table.ForeignKey(
                        name: "FK_ContractTerms_Contract_contractID",
                        column: x => x.contractID,
                        principalTable: "Contract",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HealthcareWorkerRegions",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HealthcareWorkerid = table.Column<int>(nullable: true),
                    RegionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthcareWorkerRegions", x => x.id);
                    table.ForeignKey(
                        name: "FK_HealthcareWorkerRegions_HealthcareWorker_HealthcareWorkerid",
                        column: x => x.HealthcareWorkerid,
                        principalTable: "HealthcareWorker",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HealthcareWorkerService",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HealthcareWorkerid = table.Column<int>(nullable: false),
                    userId = table.Column<int>(nullable: false),
                    serviceId = table.Column<int>(nullable: false),
                    Price = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthcareWorkerService", x => x.id);
                    table.ForeignKey(
                        name: "FK_HealthcareWorkerService_HealthcareWorker_HealthcareWorkerid",
                        column: x => x.HealthcareWorkerid,
                        principalTable: "HealthcareWorker",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "hospitalDepartments",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepatmentsOfHospitalID = table.Column<int>(nullable: false),
                    Hospitalid = table.Column<int>(nullable: false),
                    Picture = table.Column<string>(nullable: true),
                    Background = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hospitalDepartments", x => x.id);
                    table.ForeignKey(
                        name: "FK_hospitalDepartments_Hospitals_Hospitalid",
                        column: x => x.Hospitalid,
                        principalTable: "Hospitals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChronicDiseases",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(nullable: true),
                    Patientid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChronicDiseases", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ChronicDiseases_Patient_Patientid",
                        column: x => x.Patientid,
                        principalTable: "Patient",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    TokenId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    Token = table.Column<string>(nullable: true),
                    ExpiryDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.TokenId);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChronicDiseases_Patientid",
                table: "ChronicDiseases",
                column: "Patientid");

            migrationBuilder.CreateIndex(
                name: "IX_ContractTerms_contractID",
                table: "ContractTerms",
                column: "contractID");

            migrationBuilder.CreateIndex(
                name: "IX_HealthcareWorkerRegions_HealthcareWorkerid",
                table: "HealthcareWorkerRegions",
                column: "HealthcareWorkerid");

            migrationBuilder.CreateIndex(
                name: "IX_HealthcareWorkerService_HealthcareWorkerid",
                table: "HealthcareWorkerService",
                column: "HealthcareWorkerid");

            migrationBuilder.CreateIndex(
                name: "IX_hospitalDepartments_Hospitalid",
                table: "hospitalDepartments",
                column: "Hospitalid");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointment");

            migrationBuilder.DropTable(
                name: "AppointmentDoctorClinic");

            migrationBuilder.DropTable(
                name: "AppWorktime");

            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropTable(
                name: "ChargeOrRechargeRequest");

            migrationBuilder.DropTable(
                name: "ChronicDiseases");

            migrationBuilder.DropTable(
                name: "clinicDoctors");

            migrationBuilder.DropTable(
                name: "ClinicType");

            migrationBuilder.DropTable(
                name: "ContractTerms");

            migrationBuilder.DropTable(
                name: "Conversation");

            migrationBuilder.DropTable(
                name: "departmentsOfHospitals");

            migrationBuilder.DropTable(
                name: "District");

            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.DropTable(
                name: "ExternalClinic");

            migrationBuilder.DropTable(
                name: "ExternalClinicAppointment");

            migrationBuilder.DropTable(
                name: "Favorite");

            migrationBuilder.DropTable(
                name: "FCMTokens");

            migrationBuilder.DropTable(
                name: "Governorate");

            migrationBuilder.DropTable(
                name: "HealthCareWorkerAppWorkTime");

            migrationBuilder.DropTable(
                name: "HealthcareWorkerRegions");

            migrationBuilder.DropTable(
                name: "HealthcareWorkerService");

            migrationBuilder.DropTable(
                name: "HealthcareWorkerWorkPlace");

            migrationBuilder.DropTable(
                name: "HealthWorkerRequestByUser");

            migrationBuilder.DropTable(
                name: "HospitalAppointment");

            migrationBuilder.DropTable(
                name: "HospitalClinicAddress");

            migrationBuilder.DropTable(
                name: "HospitalClinicDoctor");

            migrationBuilder.DropTable(
                name: "hospitalDepartments");

            migrationBuilder.DropTable(
                name: "MedicalAdvice");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "ProfitFromTheApp");

            migrationBuilder.DropTable(
                name: "ProfitRatios");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

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
                name: "SpecialityHealthWorker");

            migrationBuilder.DropTable(
                name: "SpeciallyDoctors");

            migrationBuilder.DropTable(
                name: "UserContract");

            migrationBuilder.DropTable(
                name: "WorkerAppointment");

            migrationBuilder.DropTable(
                name: "WorkerSalary");

            migrationBuilder.DropTable(
                name: "Patient");

            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.DropTable(
                name: "HealthcareWorker");

            migrationBuilder.DropTable(
                name: "Hospitals");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
