using HealLink.Domain.Prescriptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace HealLink.Infrastructure.Persistence.Configurations.Prescriptions
{
    public class MedicationConfiguration : IEntityTypeConfiguration<Medication>
    {
        public void Configure(EntityTypeBuilder<Medication> builder)
        {


            builder
                .ToTable("Medications")
                .HasKey(m => m.Id);

            builder.HasIndex(m => m.PrescriptionId)
                .HasDatabaseName("IX_Medications_PrescriptionId");


        }
    }
}
