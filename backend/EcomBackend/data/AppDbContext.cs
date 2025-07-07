using EcomBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace EcomBackend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<CartItem> CartItems => Set<CartItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics", ParentId = null },
                new Category { Id = 2, Name = "Computers", ParentId = 1 },
                new Category { Id = 3, Name = "Laptops", ParentId = 2 },
                new Category { Id = 4, Name = "Desktops", ParentId = 2 },
                new Category { Id = 5, Name = "Smartphones", ParentId = 1 },
                new Category { Id = 6, Name = "Home Appliances", ParentId = null },
                new Category { Id = 7, Name = "Kitchen", ParentId = 6 },
                new Category { Id = 8, Name = "Refrigerators", ParentId = 7 }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Gaming Laptop X1", Price = 1299.99m, AvailabilityQty = 10, CategoryId = 3 },
                new Product { Id = 2, Name = "Ultrabook Z3", Price = 999.99m, AvailabilityQty = 5, CategoryId = 3 },
                new Product { Id = 3, Name = "All-in-One Desktop A5", Price = 749.50m, AvailabilityQty = 7, CategoryId = 4 },
                new Product { Id = 4, Name = "Smartphone S20", Price = 799.00m, AvailabilityQty = 15, CategoryId = 5 },
                new Product { Id = 5, Name = "Mini Fridge M100", Price = 199.99m, AvailabilityQty = 3, CategoryId = 8 }
            );

            modelBuilder.Entity<Category>()
                .HasOne(c => c.Parent)
                .WithMany(c => c.Children)
                .HasForeignKey(c => c.ParentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}