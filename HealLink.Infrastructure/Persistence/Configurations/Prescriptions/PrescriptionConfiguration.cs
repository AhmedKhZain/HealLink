using HealLink.Domain.Prescriptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Infrastructure.Persistence.Configurations.Prescriptions
{
    public class PrescriptionConfiguration : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Prescription> builder)
        {
            builder.ToTable("Prescriptions");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.PrescriptionText)
                .IsRequired()
                .HasMaxLength(2000);
            builder.Property(p => p.CreatedAt)
                .IsRequired();


            builder.HasMany(p => p.Medications)
                   .WithOne(m => m.Prescription)
                   .HasForeignKey(m => m.PrescriptionId)
                   .OnDelete(DeleteBehavior.Cascade);


        }
    }

}
