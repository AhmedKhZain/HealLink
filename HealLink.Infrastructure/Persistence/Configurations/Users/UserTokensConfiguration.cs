using HealLink.Domain.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Infrastructure.Persistence.Configurations.Users
{
    internal class UserTokensConfiguration : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<UserToken> builder)
        {
            builder.ToTable("UserTokens");
            builder.HasKey(ut => new {ut.Id ,ut.UserId,ut.Type});
            builder.HasOne(t=> t.User)
                .WithMany()
                .HasForeignKey(ut => ut.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(t => t.Type)
                .HasConversion(
                t => t.Name,
                t => TokenTypes.FromName(t, true));
            builder.Property(t => t.Type)
                .HasMaxLength(50);
            builder.HasIndex(t=> t.Type)
                .HasDatabaseName("IX_UserTokens_Type");
            builder.HasIndex(t=>t.UserId)
                .HasDatabaseName("IX_UserTokens_UserId");
            builder.Property(t => t.Token)
                .HasMaxLength(129);






        }
    }
}
