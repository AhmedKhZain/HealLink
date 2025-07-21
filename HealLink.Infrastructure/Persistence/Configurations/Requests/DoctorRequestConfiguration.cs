using HealLink.Domain.Doctors;
using HealLink.Domain.Patients;
using HealLink.Domain.Payments;
using HealLink.Domain.Requests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Infrastructure.Persistence.Configurations.Requests
{
    public class DoctorRequestConfiguration : IEntityTypeConfiguration<DoctorRequest>
    {
        public void Configure(EntityTypeBuilder<DoctorRequest> builder)
        {

            builder.HasKey(d => d.Id);


            builder.Property(r => r.Type)
                .IsRequired()
                .HasConversion(
                    t => t.Name,
                    t => RequestType.FromName(t, true));

            builder.Property(r => r.Plan)
                .IsRequired()
                .HasConversion(
                    p => p.Name,
                    p => DoctorSubscriptionPlan.FromName(p, true));


            builder.Property(r => r.AttachedFileLink)
                .HasMaxLength(12000)
                .IsRequired(false);

            builder.Property(r => r.RequestDate)
                .IsRequired();
            builder.Property(r => r.DecisionDate)
                .IsRequired(false);
            builder.Property(r => r.IsAccepted)
                .IsRequired(false)
                .HasDefaultValue(null);



            builder.Ignore(d => d.IsPending);
            builder.Ignore(d => d.IsRejected);
            builder.Ignore(d => d.IsApproved);



            builder.HasOne(d => d.Sender)
                .WithMany(p => p.DoctorRequests)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.Cascade);



            builder.HasOne(d => d.Doctor)
                .WithMany(dr => dr.DoctorRequests)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(d => d.Payment)
                .WithOne(p => p.DoctorRequest)
                .HasForeignKey<Payment>(d => d.DoctorRequestId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(r => r.SenderId);
            builder.HasIndex(r => r.DoctorId);




        }
    }
}
