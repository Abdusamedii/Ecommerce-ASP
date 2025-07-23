
using Ecomm.Models;
using Microsoft.EntityFrameworkCore;
namespace Ecomm.Data
{
    public class DatabaseConnection : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DatabaseConnection(DbContextOptions<DatabaseConnection> options)  : base(options)
        {
        
        }
    
    }    
}
