
using Ecomm.Models;
using Microsoft.EntityFrameworkCore;
namespace Ecomm.Data
{
    public class DatabaseConnection : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DatabaseConnection(DbContextOptions<DatabaseConnection> options)  : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>()
                .HasOne(a => a.User)
                .WithMany(u => u.Addresses)
                .HasForeignKey(a => a.UserId);
        }
    
    }    
}
