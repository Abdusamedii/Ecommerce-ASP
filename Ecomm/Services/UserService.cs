using Ecomm.Data;
using Ecomm.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecomm.Services;

public class UserService
{
    private readonly DatabaseConnection DbContext;

    public UserService(DatabaseConnection db)
    {
        DbContext = db;
    }

    public List<User> GetAllUsers()
    {
        return DbContext.Users.ToList();
    }

    public User? GetUserById(int id)
    {
        return DbContext.Users.Find(id);
    }

    public User? GetUserByEmail(string email)
    {
        return DbContext.Users.Find(email);
    }

    public async  Task<User> SaveUser(User user)
    {
        var savedUser = await  DbContext.Users.AddAsync(user);
        await DbContext.SaveChangesAsync();
        return savedUser.Entity;
    }
}