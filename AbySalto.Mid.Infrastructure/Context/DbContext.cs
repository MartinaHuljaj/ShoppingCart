using Microsoft.EntityFrameworkCore;
using AbySalto.Mid.Domain.Entities;

namespace AbySalto.Mid.Infrastructure.Context
{
    public class AbySaltoDbContext(DbContextOptions<AbySaltoDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<FavoriteItem> FavoriteItems { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Product> Products { get; set; }
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
            modelBuilder.Entity<FavoriteItem>()
                .HasOne(fp => fp.User)
                .WithMany(u => u.FavoriteItems)
                .HasForeignKey(fp => fp.UserId);

            modelBuilder.Entity<FavoriteItem>()
                .HasOne(fp => fp.Product)
                .WithMany()
                .HasForeignKey(fp => fp.ProductId);

            modelBuilder.Entity<BasketItem>()
                .HasOne(bi => bi.User)
                .WithMany(u => u.BasketItems)
                .HasForeignKey(ci => ci.UserId);
            modelBuilder.Entity<BasketItem>()
                .HasOne(bi => bi.Product)
                .WithMany()
                .HasForeignKey(ci => ci.ProductId);
            modelBuilder.Entity<Product>()
                .Property(p => p.ProductId)
                .ValueGeneratedNever();
        }
    }
}
