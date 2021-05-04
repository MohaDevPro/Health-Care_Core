﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Health_Care.Models;

namespace Health_Care.Data
{
    public class Health_CareContext : DbContext
    {
        public Health_CareContext (DbContextOptions<Health_CareContext> options)
            : base(options)
        {
        }

        public DbSet<Health_Care.Models.Appointment> Appointment { get; set; }

        public DbSet<Health_Care.Models.HealthcareWorkerWorkPlace> HealthcareWorkerWorkPlace { get; set; }

        public DbSet<Health_Care.Models.WorkerSalary> WorkerSalary { get; set; }

        public DbSet<Health_Care.Models.WorkerAppointment> WorkerAppointment { get; set; }

        public DbSet<Health_Care.Models.UserContract> UserContract { get; set; }

        public DbSet<Health_Care.Models.User> User { get; set; }

        public DbSet<Health_Care.Models.Speciality> Speciality { get; set; }

        public DbSet<Health_Care.Models.Service> Service { get; set; }

        public DbSet<Health_Care.Models.Role> Role { get; set; }

        public DbSet<Health_Care.Models.ReportAndComplaint> ReportAndComplaint { get; set; }

        public DbSet<Health_Care.Models.Region> Region { get; set; }

        public DbSet<Health_Care.Models.ClinicType> ClinicType { get; set; }

        public DbSet<Health_Care.Models.ChargeOrRechargeRequest> ChargeOrRechargeRequest { get; set; }

        public DbSet<Health_Care.Models.AppWorktime> AppWorktime { get; set; }

        public DbSet<Health_Care.Models.Contract> Contract { get; set; }

        public DbSet<Health_Care.Models.HospitalAppointment> HospitalAppointment { get; set; }

        public DbSet<Health_Care.Models.Patient> Patient { get; set; }

        public DbSet<Health_Care.Models.Conversation> Conversation { get; set; }

        public DbSet<Health_Care.Models.HealthcareWorkerService> HealthcareWorkerService { get; set; }

        public DbSet<Health_Care.Models.ProfitFromTheApp> ProfitFromTheApp { get; set; }

        public DbSet<Health_Care.Models.Doctor> Doctor { get; set; }

        public DbSet<Health_Care.Models.ExternalClinicAppointment> ExternalClinicAppointment { get; set; }

        public DbSet<Health_Care.Models.HospitalClinicDoctor> HospitalClinicDoctor { get; set; }

        public DbSet<Health_Care.Models.ExternalClinic> ExternalClinic { get; set; }

        public DbSet<Health_Care.Models.HospitalClinic> HospitalClinic { get; set; }

        public DbSet<Health_Care.Models.HealthcareWorker> HealthcareWorker { get; set; }

        public DbSet<Health_Care.Models.Blogs> Blogs { get; set; }
    }
}