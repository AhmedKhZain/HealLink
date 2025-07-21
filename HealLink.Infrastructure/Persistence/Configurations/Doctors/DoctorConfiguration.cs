using HealLink.Domain.Admins;
using HealLink.Domain.Doctors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Infrastructure.Persistence.Configurations.Doctors
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {


            builder.ToTable("Doctors");

            builder.HasKey(d => d.Id);
            builder.Property(d => d.Id)
                .ValueGeneratedNever();
            builder.Property(d => d.SyndicateIdImageLink)
                .IsRequired()
                .HasMaxLength(1200);
            builder.Property(d => d.NationalID)
                .IsRequired()
                .HasMaxLength(15);
            builder.Property(d => d.PracticeLicenseNumber)
                .IsRequired()
                .HasMaxLength(30);

            builder.HasOne(d => d.User)
                .WithOne()
                .HasForeignKey<Doctor>(d => d.Id)
                .OnDelete(DeleteBehavior.Cascade);




            builder.HasMany(d => d.Subscriptions)
                .WithOne(s => s.Doctor)
                .HasForeignKey(s => s.DoctorId)
                .HasPrincipalKey(d => d.Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(d => d.DoctorRequests)
                .WithOne(r => r.Doctor)
                .HasForeignKey(r => r.DoctorId)
                .HasPrincipalKey(d => d.Id)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
