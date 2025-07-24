using Ecomm.Data;
using Ecomm.DTO;
using Ecomm.Exceptions;
using Ecomm.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Ecomm.Services;

public class UserService
{
    private readonly DatabaseConnection _dbContext;

    public UserService(DatabaseConnection db)
    {
        _dbContext = db;
    }
    public async  Task<ServiceResult<User>> SaveUser(SignUpDTO user)
    {
        var savedUser = user.Adapt<User>();
        await _dbContext.Users.AddAsync(savedUser);
        await _dbContext.SaveChangesAsync();
        return new ServiceResult<User>(){success = true, data = savedUser};
    }
}