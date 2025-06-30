using Microsoft.EntityFrameworkCore;
using AbySalto.Mid.Domain.Entities;

namespace AbySalto.Mid.Domain.DatabaseContext
{
    public class AbySaltoDbContext(DbContextOptions<AbySaltoDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("DefaultConnection");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
