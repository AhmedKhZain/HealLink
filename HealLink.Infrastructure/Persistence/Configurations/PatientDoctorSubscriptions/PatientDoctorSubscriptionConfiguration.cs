using HealLink.Domain.PatientDoctorSubscriptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Infrastructure.Persistence.Configurations.PatientDoctorSubscriptions
{
    public class PatientDoctorSubscriptionConfiguration : IEntityTypeConfiguration<PatientDoctorSubscription>
    {
        public void Configure(EntityTypeBuilder<PatientDoctorSubscription> builder)
        {
            
            builder.ToTable("PatientDoctorSubscriptions");
            builder.HasKey(pds => pds.Id);


            builder.Property(pds => pds.PatientId)
                .IsRequired();
            builder.Property(pds => pds.DoctorId)
                .IsRequired();
            builder.Property(pds => pds.SubscribedAt)
                .IsRequired();



            builder.HasOne(pds => pds.Patient)
                .WithMany(p => p.DoctorPatients)
                .HasForeignKey(pds => pds.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(pds => pds.Doctor)
                .WithMany(d => d.Subscriptions)
                .HasForeignKey(pds => pds.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);




            builder.HasMany(pds => pds.ChatMassages)
                .WithOne(c => c.PatientDoctorSubscription)
                .HasForeignKey(c => c.PatientDoctorSubscriptionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(pds => pds.DoctorRequests)
                .WithOne(dr => dr.Subscription)
                .HasForeignKey(dr => dr.SubscriptionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(pds => pds.Prescriptions)
                .WithOne(p => p.Subscription)
                .HasForeignKey(p => p.SubsciptionId)
                .OnDelete(DeleteBehavior.Cascade);



            builder.HasIndex(builder => builder.DoctorId)
                .HasDatabaseName("IX_PatientDoctorSubscriptions_DoctorId");
            builder.HasIndex(builder => builder.PatientId)
                .HasDatabaseName("IX_PatientDoctorSubscriptions_PatientId");



        }
    }
}
