﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyApp.Domain.Entities;

namespace PropertyApp.Infrastructure.Configuration;

internal class UserConfiguration : IEntityTypeConfiguration<User>

{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Email).IsRequired();
        builder.Property(u => u.LastName).IsRequired().HasMaxLength(50);
        builder.Property(u => u.FirstName).IsRequired().HasMaxLength(50);           
        builder.Property(u => u.CreatedDate).IsRequired().HasColumnType("smalldatetime");
        builder.Property(u => u.LastModifiedDate).HasColumnType("smalldatetime");
        builder.Ignore(u => u.CreatedBy);
        builder.HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId);
    }
}
