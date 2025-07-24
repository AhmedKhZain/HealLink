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
    public class HealLinkDbContext:DbContext,IUnitOfWork
    {

        private IDbContextTransaction? _transaction;

        public HealLinkDbContext(DbContextOptions<HealLinkDbContext> options) : base(options)
        {
        }

        public async Task CommitChangesAsync()
            => await SaveChangesAsync();

        public async Task StartTransactionAsync()
        {
            if (_transaction is not null)
                return;

            _transaction = await Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await CommitChangesAsync();
            if (_transaction is not null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction is not null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task<ErrorOr<Success>> ExecuteInTransactionAsync(Func<Task> action)
        {
            await StartTransactionAsync();

            List<Error> errors = [];

            try
            {
                await action();
                await CommitChangesAsync();
                await CommitTransactionAsync();

                return new Success();
            }
            catch (Exception ex)
            {
                errors.Add(Error.Failure(code: ex.Source ?? "Transaction", description: ex.Message));

                try
                {
                    await RollbackTransactionAsync();
                }
                catch (Exception rollbackEx)
                {
                    errors.Add(Error.Failure(code: rollbackEx.Source ?? "Rollback", description: rollbackEx.Message));
                }

                return ErrorOr<Success>.From(errors);
            }
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
        public DbSet<Medication> Medications { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }

    }
}
