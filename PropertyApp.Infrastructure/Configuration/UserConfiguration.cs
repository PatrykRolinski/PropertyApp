using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyApp.Infrastructure.Configuration
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>

    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Email).IsRequired();
            builder.Property(u => u.LastName).IsRequired().HasMaxLength(50);
            builder.Property(u => u.FirstName).IsRequired().HasMaxLength(50);           
            builder.Property(u => u.CreatedDate).IsRequired().HasColumnType("smalldatetime");
            builder.Property(u => u.LastModifiedDate).HasColumnType("smalldatetime");
            builder.HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);
        }
    }
}
