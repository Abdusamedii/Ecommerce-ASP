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
    }
}