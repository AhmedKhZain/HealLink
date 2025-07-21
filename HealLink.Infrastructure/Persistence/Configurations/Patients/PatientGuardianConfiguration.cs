using HealLink.Domain.Patients.HealLink.Domain.Patients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Infrastructure.Persistence.Configurations.Patients
{
    public class PatientGuardianConfiguration : IEntityTypeConfiguration<PatientGuardian>
    {
        public void Configure(EntityTypeBuilder<PatientGuardian> builder)
        {

            builder.ToTable("PatientGuardians");
            builder.HasKey(pg => pg.Id);
            builder.Property(pg => pg.Id)
                .ValueGeneratedOnAdd();

            builder.Property(pg => pg.RelationshipType)
                .HasMaxLength(150);

            builder.HasOne(pg => pg.Patient)
                .WithMany(p=>p.Guardians)
                .HasForeignKey(pg => pg.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(pg => pg.Guardian)
                .WithMany()
                .HasForeignKey(pg => pg.GuardianId)
                .OnDelete(DeleteBehavior.Restrict);



            // Indexes 


            builder.HasIndex(pg => pg.GuardianId)
                .HasDatabaseName("IX_PatientGuardians_GuardianId");

            builder.HasIndex(pg => pg.PatientId)    
                .HasDatabaseName("IX_PatientGuardians_PatientId");




        }
    }
}
