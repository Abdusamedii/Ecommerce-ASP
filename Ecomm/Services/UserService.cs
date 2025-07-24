using Ecomm.Data;
using Ecomm.DTO;
using Ecomm.Exceptions;
using Ecomm.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Ecomm.Services;

public class UserService
{
    private readonly DatabaseConnection _dbContext;

    public UserService(DatabaseConnection db)
    {
        _dbContext = db;
    }

    public async Task<ServiceResult<User>> SaveUser(SignUpDTO user)
    {
        if (user == null) return new ServiceResult<User> { success = false, errorMessage = "User is null" };
        if (await _dbContext.Users.AsNoTracking().AnyAsync(u => u.username == user.username))
            return new ServiceResult<User> { success = false, errorMessage = "Username already exists" };
        if (user.password != user.confirmPassword)
            return new ServiceResult<User> { success = false, errorMessage = "Passwords do not match" };
        var savedUser = user.Adapt<User>();
        try
        {
            await _dbContext.Users.AddAsync(savedUser);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return new ServiceResult<User> { success = false, errorMessage = ex.Message };
        }

        return new ServiceResult<User> { success = true, data = savedUser };
    }

    public async Task<ServiceResult<List<User>>> findAll()
    {
        var users = _dbContext.Users.ToList();
        if (users.Count == 0) return new ServiceResult<List<User>> { success = false, errorMessage = "No users found" };
        return new ServiceResult<List<User>> { success = true, data = users };
    }

    public async Task<ServiceResult<User>> findUser(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            return new ServiceResult<User> { success = false, errorMessage = "Username is empty" };
        var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.username == username);
        if (user == null) return new ServiceResult<User> { success = false, errorMessage = "User not found" };
        var Addresses = _dbContext.Addresses.AsNoTracking().Where(a => a.UserId == user.id);
        user.Addresses = Addresses.ToList();
        return new ServiceResult<User> { success = true, data = user };
    }
}