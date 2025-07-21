using HealLink.Domain.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Infrastructure.Persistence.Configurations.Payments
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {


            builder.ToTable("Payments");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Amount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
            builder.Property(p => p.Status)
                .HasConversion(
                s=>s.Value,
                s => Payment.PaymentStatus.FromValue(s));

            builder.Property(p => p.PaymentProviderId)
                .HasMaxLength(160);


            builder.HasOne(p => p.DoctorRequest)
                .WithOne()
                .HasForeignKey<Payment>(p => p.DoctorRequestId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Property(p => p.PaidAt)
                .IsRequired();









        }
    }
}
