﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyApp.Domain.Entities;

namespace PropertyApp.Infrastructure.Configuration;

public class PropertyConfiguration : IEntityTypeConfiguration<Property>
{
    public void Configure(EntityTypeBuilder<Property> builder)
    {

        builder.Property(p => p.Description).HasMaxLength(2000).IsRequired();
        builder.Property(P => P.Price).IsRequired();
        builder.Property(p => p.OriginalPrice).IsRequired();
        builder.Property(P => P.PropertyType).IsRequired().HasConversion<string>();
        builder.Property(P => P.PropertySize).IsRequired();
        builder.Property(P => P.PropertyStatus).IsRequired().HasConversion<string>();
        builder.Property(P => P.MarketType).IsRequired().HasConversion<string>();
        builder.Property(P => P.CreatedDate).IsRequired().HasColumnType("smalldatetime");
        builder.Property(P => P.LastModifiedDate).HasColumnType("smalldatetime");
        

        builder.HasOne(p => p.Address)
            .WithOne(a => a.Property)
            .HasForeignKey<Property>(p => p.AddressId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Photos)
            .WithOne(ph => ph.Property)
            .HasForeignKey(ph => ph.PropertyId)
            .OnDelete(DeleteBehavior.Cascade);       

        builder.HasOne(p=> p.CreatedBy)
            .WithMany(u=> u.CreatedProperties)
            .HasForeignKey(p=> p.CreatedById)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
