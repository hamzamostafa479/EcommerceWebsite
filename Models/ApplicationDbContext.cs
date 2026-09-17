using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Ecommerce_Project.Models
{
  public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
  {
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {

    }
    public ApplicationDbContext()
    {

    }

    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      // Seed some sample categories
      modelBuilder.Entity<Category>().HasData(
          new Category { CategoryId = 1, Name = "Electronics", Description = "Electronic devices and accessories" },
          new Category { CategoryId = 2, Name = "Clothing", Description = "Apparel and fashion items" },
          new Category { CategoryId = 3, Name = "Books", Description = "Books and publications" }
      );

      // Seed some sample products
      modelBuilder.Entity<Product>().HasData(
          new Product { ProductId = 1, Name = "Wireless Mouse", Description = "Ergonomic wireless mouse", Price = 29.99m, StockQuantity = 100, CategoryId = 1, IsActive = true, ImageUrl = "https://images.unsplash.com/photo-1660491083562-d91a64d6ea9c?q=80&w=400" },
          new Product { ProductId = 2, Name = "USB-C Hub", Description = "7-in-1 USB-C hub", Price = 49.99m, StockQuantity = 50, CategoryId = 1, IsActive = true, ImageUrl = "https://plus.unsplash.com/premium_photo-1761043248662-42f371ad31b4?q=80&w=400" },
          new Product { ProductId = 3, Name = "Cotton T-Shirt", Description = "100% cotton comfortable t-shirt", Price = 19.99m, StockQuantity = 200, CategoryId = 2, IsActive = true, ImageUrl = "https://images.unsplash.com/photo-1651761179569-4ba2aa054997?q=80&w=400" },
          new Product { ProductId = 4, Name = "C# Programming Guide", Description = "Comprehensive C# programming book", Price = 39.99m, StockQuantity = 75, CategoryId = 3, IsActive = true, ImageUrl = "https://plus.unsplash.com/premium_photo-1764695579456-9e6f13928281?q=80&w=400" }
      );
    }
  }
}
