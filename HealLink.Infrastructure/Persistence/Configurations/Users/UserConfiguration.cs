using HealLink.Domain.Admins;
using HealLink.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Infrastructure.Persistence.Configurations.Users
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(U => U.Id);
            builder.Property(U => U.FullName)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(U => U.NameToShow)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(U => U.Email)
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(U => U.IsVerified)
                .IsRequired()
                .HasDefaultValue(false);
            builder.Property(U => U.ProfilePhotoLink)
                .HasMaxLength(2000)
                .IsRequired(false);
            
            builder.Property(U => U.Role)
                .IsRequired()
                .HasConversion(
                    r => r.Name, 
                    r => Role.FromName(r, true)) 
                .HasMaxLength(20);
            builder.Property(U => U._passwordHash)
                .IsRequired()
                .HasMaxLength(1000)
                .HasColumnName("PasswordHash");
            builder
            .HasIndex(u => u.Email)
            .IsUnique();

            builder.Ignore(u => u.Is2FAEnabled);



        }
    }
}
