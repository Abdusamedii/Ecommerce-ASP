using CalConnect.Api.Users.Infrastructure;
using Ecomm.Data;
using Ecomm.DTO;
using Ecomm.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Ecomm.Services;

public class AuthService
{
    private readonly DatabaseConnection _dbContext;
    private readonly TokenProvider _tokenProvider;

    public AuthService(DatabaseConnection db, TokenProvider tokenProvider)
    {
        _dbContext = db;
        _tokenProvider = tokenProvider;
    }

    public async Task<ServiceResult<string>> login(SignInDTO login)
    {
        var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.email == login.Email);
        if (user == null)
            return new ServiceResult<string>
                { success = false, errorMessage = "User not found with this email Adress" };
        if (login.Password == user.password)
        {
            var token = _tokenProvider.Create(user);
            Console.WriteLine(token);
            return new ServiceResult<string> { success = true, data = token };
        }

        return new ServiceResult<string>
            { success = false, errorMessage = "Wrong password" };
    }
}