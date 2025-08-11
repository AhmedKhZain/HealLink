using ErrorOr;
using healLink.Application.Common.Interfaces.Service;
using HealLink.Domain.Admins;
using HealLink.Domain.Doctors;
using HealLink.Domain.MedicalHistories;
using HealLink.Domain.PatientDoctorSubscriptions;
using HealLink.Domain.Patients;
using HealLink.Domain.Patients.HealLink.Domain.Patients;
using HealLink.Domain.Payments;
using HealLink.Domain.Prescriptions;
using HealLink.Domain.Requests;
using HealLink.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Transactions;

namespace HealLink.Infrastructure.Persistence
{
    public class HealLinkDbContext:DbContext
    {


        public HealLinkDbContext(DbContextOptions<HealLinkDbContext> options) : base(options)
        {
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HealLinkDbContext).Assembly);

        }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientDoctorSubscription> PatientDoctorSubscriptions { get; set; }
        public DbSet<DoctorRequest> DoctorRequests { get; set; }
        public DbSet<PatientGuardian> PatientGuardians { get; set; }
        public DbSet<MedicalHistory> MedicalHistories { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<PatientDoctorSubscriptionChatMessage> PatientDoctorSubscriptionChatMessages { get; set; }
        //public DbSet<Medication> Medications { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        public DbSet<RefundItem> RefundItems { get; set; }

    }
}
