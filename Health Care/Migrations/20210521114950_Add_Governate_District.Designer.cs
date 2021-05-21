﻿// <auto-generated />
using System;
using Health_Care.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Health_Care.Migrations
{
    [DbContext(typeof(Health_CareContext))]
<<<<<<< HEAD:Health Care/Migrations/20210521114950_Add_Governate_District.Designer.cs
    [Migration("20210521114950_Add_Governate_District")]
    partial class Add_Governate_District
=======
    [Migration("20210518031655_editAppointmentProfitfromAppModel")]
    partial class editAppointmentProfitfromAppModel
>>>>>>> origin/master:Health Care/Migrations/20210518031655_editAppointmentProfitfromAppModel.Designer.cs
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Health_Care.Models.AppWorktime", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RealClossTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RealOpenTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("clinicId")
                        .HasColumnType("int");

                    b.Property<int>("day")
                        .HasColumnType("int");

                    b.Property<int>("endTime")
                        .HasColumnType("int");

                    b.Property<string>("shiftAM_PM")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<int>("startTime")
                        .HasColumnType("int");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("AppWorktime");
                });

            modelBuilder.Entity("Health_Care.Models.Appointment", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Accepted")
                        .HasColumnType("bit");

                    b.Property<bool>("Paid")
                        .HasColumnType("bit");

                    b.Property<bool>("PatientComeToAppointment")
                        .HasColumnType("bit");

                    b.Property<bool>("PercentageFromAppointmentPriceForApp")
                        .HasColumnType("bit");

                    b.Property<string>("TypeOfAppointment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("appointmentDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("appointmentForUserHimself")
                        .HasColumnType("bit");

                    b.Property<int>("appointmentPrice")
                        .HasColumnType("int");

                    b.Property<string>("appointmentStartFrom")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("appointmentUntilTo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("cancelReasonWrittenBySecretary")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("cancelledByClinicSecretary")
                        .HasColumnType("bit");

                    b.Property<bool>("cancelledByUser")
                        .HasColumnType("bit");

                    b.Property<int>("distnationClinicId")
                        .HasColumnType("int");

                    b.Property<int>("doctorId")
                        .HasColumnType("int");

                    b.Property<string>("patientName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("patientphone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.Property<string>("visitReason")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Appointment");
                });

            modelBuilder.Entity("Health_Care.Models.AppointmentDoctorClinic", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("appointmentDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("clinicId")
                        .HasColumnType("int");

                    b.Property<int>("doctorId")
                        .HasColumnType("int");

                    b.Property<int>("numberOfAvailableAppointment")
                        .HasColumnType("int");

                    b.Property<int>("numberOfRealAppointment")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("AppointmentDoctorClinic");
                });

            modelBuilder.Entity("Health_Care.Models.Blogs", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("Health_Care.Models.ChargeOrRechargeRequest", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BalanceReceipt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ConfirmToAddBalance")
                        .HasColumnType("bit");

                    b.Property<string>("rechargeDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("ChargeOrRechargeRequest");
                });

            modelBuilder.Entity("Health_Care.Models.ClinicDoctor", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Clinicid")
                        .HasColumnType("int");

                    b.Property<int>("Doctorid")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("clinicDoctors");
                });

            modelBuilder.Entity("Health_Care.Models.ClinicType", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClinicTypeName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("ClinicType");
                });

            modelBuilder.Entity("Health_Care.Models.Contract", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("contractFor")
                        .HasColumnType("int");

                    b.Property<string>("contractPath")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Contract");
                });

            modelBuilder.Entity("Health_Care.Models.Conversation", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("appointmentId")
                        .HasColumnType("int");

                    b.Property<bool>("isReaded")
                        .HasColumnType("bit");

                    b.Property<bool>("isRecived")
                        .HasColumnType("bit");

                    b.Property<string>("message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("messageDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("messageTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("userIdFrom")
                        .HasColumnType("int");

                    b.Property<int>("userIdTo")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Conversation");
                });

            modelBuilder.Entity("Health_Care.Models.DepartmentsOfHospital", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("departmentsOfHospitals");
                });

            modelBuilder.Entity("Health_Care.Models.District", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("District");
                });

            modelBuilder.Entity("Health_Care.Models.Doctor", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Picture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Userid")
                        .HasColumnType("int");

                    b.Property<int>("appointmentPrice")
                        .HasColumnType("int");

                    b.Property<string>("backgroundImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("graduationCertificateImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("identificationImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("numberOfAvailableAppointment")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Doctor");
                });

            modelBuilder.Entity("Health_Care.Models.ExternalClinic", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BackgoundImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ClinicTypeId")
                        .HasColumnType("int");

                    b.Property<int>("HospitalDepartmentsID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Picture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("doctorId")
                        .HasColumnType("int");

                    b.Property<int>("numberOfAvailableAppointment")
                        .HasColumnType("int");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("ExternalClinic");
                });

            modelBuilder.Entity("Health_Care.Models.ExternalClinicAppointment", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("PatientComeToAppointment")
                        .HasColumnType("bit");

                    b.Property<int>("appointmentId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("ExternalClinicAppointment");
                });

            modelBuilder.Entity("Health_Care.Models.FCM_Tokens", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DeviceID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("FCMTokens");
                });

            modelBuilder.Entity("Health_Care.Models.Favorite", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Favorite");
                });

            modelBuilder.Entity("Health_Care.Models.Governorate", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Governorate");
                });

            modelBuilder.Entity("Health_Care.Models.HealthcareWorker", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BackGroundPicture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Picture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ReagionID")
                        .HasColumnType("int");

                    b.Property<string>("WorkPlace")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("graduationCertificateImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("identificationImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("speciality")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("HealthcareWorker");
                });

            modelBuilder.Entity("Health_Care.Models.HealthcareWorkerService", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("HealthcareWorkerid")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("serviceId")
                        .HasColumnType("int");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("HealthcareWorkerid");

                    b.ToTable("HealthcareWorkerService");
                });

            modelBuilder.Entity("Health_Care.Models.HealthcareWorkerWorkPlace", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Day")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("endTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("shiftAM_PM")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("startTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.Property<string>("workePlaceName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("HealthcareWorkerWorkPlace");
                });

            modelBuilder.Entity("Health_Care.Models.Hospital", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BackgoundImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Picture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("hospitalId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Hospitals");
                });

            modelBuilder.Entity("Health_Care.Models.HospitalAppointment", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("appointmentId")
                        .HasColumnType("int");

                    b.Property<int>("clinicId")
                        .HasColumnType("int");

                    b.Property<int>("doctorid")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("HospitalAppointment");
                });

            modelBuilder.Entity("Health_Care.Models.HospitalClinicDoctor", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Day")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("doctorId")
                        .HasColumnType("int");

                    b.Property<string>("endTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("hospitalClinicId")
                        .HasColumnType("int");

                    b.Property<string>("startTime")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("HospitalClinicDoctor");
                });

            modelBuilder.Entity("Health_Care.Models.HospitalDepartments", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Background")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DepatmentsOfHospitalID")
                        .HasColumnType("int");

                    b.Property<int>("Hospitalid")
                        .HasColumnType("int");

                    b.Property<string>("Picture")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("Hospitalid");

                    b.ToTable("hospitalDepartments");
                });

            modelBuilder.Entity("Health_Care.Models.MedicalAdvice", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("paragraph")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("MedicalAdvice");
                });

            modelBuilder.Entity("Health_Care.Models.Notifications", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("body")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isRepeated")
                        .HasColumnType("bit");

                    b.Property<string>("time")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("Health_Care.Models.Patient", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Balance")
                        .HasColumnType("int");

                    b.Property<string>("LastBalanceChargeDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("birthDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("chronicDiseases")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Patient");
                });

            modelBuilder.Entity("Health_Care.Models.ProfitFromTheApp", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("clinicId")
                        .HasColumnType("int");

                    b.Property<int>("doctorId")
                        .HasColumnType("int");

                    b.Property<int>("hospitalId")
                        .HasColumnType("int");

                    b.Property<int>("profit")
                        .HasColumnType("int");

                    b.Property<int>("sumOfAppointment")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("ProfitFromTheApp");
                });

            modelBuilder.Entity("Health_Care.Models.ProfitRatios", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("medicalExaminationPercentage")
                        .HasColumnType("int");

                    b.Property<int>("servicePercentage")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("ProfitRatios");
                });

            modelBuilder.Entity("Health_Care.Models.RefreshToken", b =>
                {
                    b.Property<int>("TokenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("TokenId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("Health_Care.Models.Region", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("regionName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Region");
                });

            modelBuilder.Entity("Health_Care.Models.ReportAndComplaint", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("complaintTypeid")
                        .HasColumnType("int");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("ReportAndComplaint");
                });

            modelBuilder.Entity("Health_Care.Models.Role", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("Health_Care.Models.Service", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("serviceName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Service");
                });

            modelBuilder.Entity("Health_Care.Models.Speciality", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BasicSpecialityID")
                        .HasColumnType("int");

                    b.Property<bool>("isBasic")
                        .HasColumnType("bit");

                    b.Property<string>("specialityName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Speciality");
                });

            modelBuilder.Entity("Health_Care.Models.SpeciallyDoctor", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Doctorid")
                        .HasColumnType("int");

                    b.Property<int>("Roleid")
                        .HasColumnType("int");

                    b.Property<int>("Specialityid")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("SpeciallyDoctors");
                });

            modelBuilder.Entity("Health_Care.Models.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DeviceId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Roleid")
                        .HasColumnType("int");

                    b.Property<string>("address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isActiveAccount")
                        .HasColumnType("bit");

                    b.Property<string>("nameAR")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nameEN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("regionId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Health_Care.Models.UserContract", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("contractEndDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("contractId")
                        .HasColumnType("int");

                    b.Property<string>("contractStartDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("UserContract");
                });

            modelBuilder.Entity("Health_Care.Models.WorkerAppointment", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("acceptedAppointment")
                        .HasColumnType("bit");

                    b.Property<int>("appointmentId")
                        .HasColumnType("int");

                    b.Property<int>("regionId")
                        .HasColumnType("int");

                    b.Property<int>("serviceId")
                        .HasColumnType("int");

                    b.Property<int>("workerId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("WorkerAppointment");
                });

            modelBuilder.Entity("Health_Care.Models.WorkerSalary", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("isPaid")
                        .HasColumnType("bit");

                    b.Property<int>("salary")
                        .HasColumnType("int");

                    b.Property<int>("workerId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("WorkerSalary");
                });

            modelBuilder.Entity("Health_Care.Models.HealthcareWorkerService", b =>
                {
                    b.HasOne("Health_Care.Models.HealthcareWorker", null)
                        .WithMany("HealthcareWorkerServices")
                        .HasForeignKey("HealthcareWorkerid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Health_Care.Models.HospitalDepartments", b =>
                {
                    b.HasOne("Health_Care.Models.Hospital", null)
                        .WithMany("hospitalDepartments")
                        .HasForeignKey("Hospitalid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Health_Care.Models.RefreshToken", b =>
                {
                    b.HasOne("Health_Care.Models.User", "User")
                        .WithOne("RefreshTokens")
                        .HasForeignKey("Health_Care.Models.RefreshToken", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
