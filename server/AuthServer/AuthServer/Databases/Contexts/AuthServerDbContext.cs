using System;
using AuthServer.Databases.Seeds;
using AuthServer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthServer.Databases.Contexts
{
    public class AuthServerDbContext : DbContext
    {
        public AuthServerDbContext()
        {
            
        }
        public AuthServerDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity((Action<EntityTypeBuilder<User>>)(b =>
            {
                b.ToTable("User");
                b.HasData(SeedData.BuildUserSettings());
            }));
        }
    }
}
