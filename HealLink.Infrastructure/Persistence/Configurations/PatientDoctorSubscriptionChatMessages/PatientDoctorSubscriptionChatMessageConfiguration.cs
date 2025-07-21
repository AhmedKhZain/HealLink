using HealLink.Domain.PatientDoctorSubscriptionChatMessages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Infrastructure.Persistence.Configurations.PatientDoctorSubscriptionChatMessages
{
    public class PatientDoctorSubscriptionChatMessageConfiguration : IEntityTypeConfiguration<PatientDoctorSubscriptionChatMessage>
    {
        public void Configure(EntityTypeBuilder<PatientDoctorSubscriptionChatMessage> builder)
        {
            builder.ToTable("SubscriptionChatMessages");
            builder.HasKey(m => m.Id);

            builder.Property(pds=> pds.PatientDoctorSubscriptionId)
                .IsRequired();
            builder.Property(pds => pds.Message)
                .IsRequired()
                .HasMaxLength(1200);
            builder.Property(pds => pds.DateTime)
                .IsRequired();



            builder.HasIndex(m => m.PatientDoctorSubscriptionId)
                .HasDatabaseName("IX_SubscriptionChatMessages_SubscriptionId");




        }
    }
}
