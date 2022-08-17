using Microsoft.EntityFrameworkCore;
using PropertyApp.Domain.Entities;
using PropertyApp.Infrastructure.Configuration;


namespace PropertyApp.Infrastructure;

public class PropertyAppContext : DbContext
{
    public PropertyAppContext(DbContextOptions<PropertyAppContext> options) : base(options) { }

    public DbSet<Property> Properties  => Set<Property>();
    public DbSet<Address> Addresses => Set<Address>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Photo> Photos => Set<Photo>();
    public DbSet<LikeProperty> LikedProperties => Set<LikeProperty>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new PropertyConfiguration().Configure(modelBuilder.Entity<Property>());
        new AddressConfiguration().Configure(modelBuilder.Entity<Address>());
        new UserConfiguration().Configure(modelBuilder.Entity<User>());
        new RoleConfiguration().Configure(modelBuilder.Entity<Role>());
        new PhotoConfiguration().Configure(modelBuilder.Entity<Photo>());
        new LikeConfiguration().Configure(modelBuilder.Entity<LikeProperty>());
    }
}
