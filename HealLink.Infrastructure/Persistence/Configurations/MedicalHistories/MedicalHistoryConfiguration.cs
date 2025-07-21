using HealLink.Domain.MedicalHistories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Infrastructure.Persistence.Configurations.MedicalHistories
{
    public class MedicalHistoryConfiguration : IEntityTypeConfiguration<MedicalHistory>
    {
        public void Configure(EntityTypeBuilder<MedicalHistory> builder)
        {

            builder.ToTable("MedicalHistories");
            builder.HasKey(mh => mh.Id);


            builder.Property(mh => mh.PatientId)
                .IsRequired();
            builder.Property(mh => mh.Description)
                .HasMaxLength(1500);
            builder.Property(mh => mh.FileLink)
                .HasMaxLength(1500);


            builder.Property(mh => mh.Type)
                .HasConversion(
                    v => v.Name,
                    v => MedicalHistoryType.FromName(v,true))
                .IsRequired();



            builder.HasIndex(builder => builder.PatientId)
                .HasDatabaseName("IX_MedicalHistories_PatientId");



        }
    }
}
