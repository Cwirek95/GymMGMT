using GymMGMT.Domain.Entities;
using GymMGMT.Persistence.EF.Configurations;
using Microsoft.EntityFrameworkCore;

namespace GymMGMT.Persistence.EF
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IRepositoryConfiguration<>).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
