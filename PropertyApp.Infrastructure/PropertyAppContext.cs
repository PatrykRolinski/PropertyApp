using Microsoft.EntityFrameworkCore;
using PropertyApp.Domain.Entities;
using PropertyApp.Infrastructure.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyApp.Infrastructure
{
    public class PropertyAppContext : DbContext
    {
        public PropertyAppContext(DbContextOptions<PropertyAppContext> options) : base(options) { }

        public DbSet<Property> Properties { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Photo> Photos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new PropertyConfiguration().Configure(modelBuilder.Entity<Property>());
            new AddressConfiguration().Configure(modelBuilder.Entity<Address>());
            new UserConfiguration().Configure(modelBuilder.Entity<User>());
            new RoleConfiguration().Configure(modelBuilder.Entity<Role>());
            new PhotoConfiguration().Configure(modelBuilder.Entity<Photo>());
        }
    }
}
