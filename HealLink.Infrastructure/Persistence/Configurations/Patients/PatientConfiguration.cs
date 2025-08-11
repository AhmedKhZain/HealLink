using HealLink.Domain.Doctors;
using HealLink.Domain.Patients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Infrastructure.Persistence.Configurations.Patients
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patients");

            builder.Property(p=> p.Id).IsRequired();
            builder.HasKey(p => p.Id);


            
            builder.HasOne(d => d.User)
                .WithOne()
                .HasForeignKey<Patient>(p => p.Id)
                .OnDelete(DeleteBehavior.Restrict);

            // Subscriptions
            builder.HasMany(p => p.Subscriptions)
                .WithOne(s => s.Patient)
                .HasForeignKey(s => s.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Medical Histories
            builder.HasMany(p => p.MedicalHistories)
                .WithOne(m => m.Patient)
                .HasForeignKey(m => m.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            // Doctor Requests
            builder.HasMany(p => p.DoctorRequests)
                .WithOne(d => d.Sender)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
