using HealLink.Domain.Admins;
using HealLink.Domain.Doctors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Infrastructure.Persistence.Configurations.Admins
{
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {


            builder.ToTable("Admins");
            
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .ValueGeneratedNever();

            builder.HasMany(a => a._ApprovedDoctors)
                .WithOne(d=> d.Admin)
                .HasForeignKey(d => d.ApprovedByAdminId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.User)
                .WithOne()
                .HasForeignKey<Admin>(a=>a.Id)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
