using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyApp.Domain.Entities;

namespace PropertyApp.Infrastructure.Configuration;

public class LikeConfiguration : IEntityTypeConfiguration<LikeProperty>
{
    public void Configure(EntityTypeBuilder<LikeProperty> builder)
    {
        builder.HasKey(lp => new {lp.UserId, lp.PropertyId});

        builder.HasOne(lp => lp.User)
            .WithMany(u => u.LikedProperties)
            .HasForeignKey(lp => lp.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(lp => lp.Property)
            .WithMany(p => p.LikedProperties)
            .HasForeignKey(lp => lp.PropertyId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
