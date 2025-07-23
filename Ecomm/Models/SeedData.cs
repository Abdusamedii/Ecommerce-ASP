using Ecomm.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecomm.Models;

public class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context =
               new DatabaseConnection(serviceProvider.GetRequiredService<DbContextOptions<DatabaseConnection>>()))
        {
            if (context.Users.Any())
            {
                System.Console.WriteLine("Users already exists");
                return;
            }
            System.Console.WriteLine("Users don't exost");
            context.Users.AddRange(
                new User
                {
                    password = "test1",
                    username = "medi"
                },
                new User
                {
                    password = "test1",
                    username = "medi"
                },
                new User
                {
                    password = "test1",
                    username = "medi"
                },
                new User
                {
                    password = "test1",
                    username = "medi"
                }
                );
            context.SaveChanges();
        }
    }
}