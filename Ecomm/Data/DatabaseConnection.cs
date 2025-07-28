using Ecomm.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecomm.Data;

public class DatabaseConnection : DbContext
{
    public DatabaseConnection(DbContextOptions<DatabaseConnection> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<SubCategory> SubCategories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductSubCategory> ProductSubCategories { get; set; }

    // public DbSet<ProductSKU> ProductSKUs { get; set; }

    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>()
            .HasOne(a => a.User)
            .WithMany(u => u.Addresses)
            .HasForeignKey(a => a.UserId);
        modelBuilder.Entity<User>()
            .HasIndex(u => u.username)
            .IsUnique();
        modelBuilder.Entity<User>().HasIndex(u => u.email).IsUnique();
        modelBuilder.Entity<Cart>()
            .HasOne(c => c.User)
            .WithOne(u => u.Cart)
            .HasForeignKey<Cart>(c => c.UserId);
        modelBuilder.Entity<SubCategory>()
            .HasOne(s => s.Category)
            .WithMany(c => c.SubCategories)
            .HasForeignKey(s => s.ParentId);
        modelBuilder.Entity<ProductSubCategory>().HasKey(pc => new { pc.ProductId, pc.SubCategoryId });
        modelBuilder.Entity<ProductSubCategory>()
            .HasOne(s => s.SubCategory)
            .WithMany(c => c.ProductCategories)
            .HasForeignKey(s => s.SubCategoryId);
        modelBuilder.Entity<ProductSubCategory>()
            .HasOne(s => s.Product)
            .WithMany(c => c.ProductCategories)
            .HasForeignKey(s => s.ProductId);
        modelBuilder.Entity<Product>()
            .HasMany(p => p.ProductImages)
            .WithOne(p => p.Product)
            .HasForeignKey(p => p.ProductId);
        modelBuilder.Entity<ProductImage>()
            .HasOne(p => p.Product)
            .WithMany(p => p.ProductImages)
            .HasForeignKey(p => p.ProductId);
        modelBuilder.Entity<CartItem>()
            .HasOne(c => c.Product)
            .WithMany(p => p.cartItems)
            .HasForeignKey(c => c.ProductId);
        modelBuilder.Entity<CartItem>()
            .HasOne(c => c.Cart)
            .WithMany(c => c.CartItems)
            .HasForeignKey(c => c.CartId);
        modelBuilder.Entity<CartItem>()
            .HasIndex(ci => new { ci.CartId, ci.ProductId })
            .IsUnique();
        // modelBuilder.Entity<ProductSKU>()
        //     .HasMany(s => s.Product)
        //     .WithOne(p => p.)
    }
}