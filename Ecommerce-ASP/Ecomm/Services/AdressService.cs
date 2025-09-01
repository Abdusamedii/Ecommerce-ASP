using Ecomm.Data;
using Ecomm.DTO;
using Ecomm.Exceptions;
using Ecomm.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Ecomm.Services;

public class AdressService
{
    private readonly DatabaseConnection _dbContext;

    public AdressService(DatabaseConnection db)
    {
        _dbContext = db;
    }

    public async Task<ServiceResult<Address>> CreateAdress(CreateAdressDTO adress)
    {
        if (adress is null) return new ServiceResult<Address> { success = false, errorMessage = "Adress is null" };
        var user = await _dbContext.Users.AsNoTracking().AnyAsync(u => u.id == adress.UserId);
        if (!user) return new ServiceResult<Address> { success = false, errorMessage = "User not found" };
        var address = adress.Adapt<Address>();
        await _dbContext.Addresses.AddAsync(address);
        var created = await _dbContext.SaveChangesAsync();

        if (created < 0) return new ServiceResult<Address> { success = false, errorMessage = "Database error" };
        /*I'm very sorry for the incorrect use of adress and address LMAO */
        return new ServiceResult<Address> { success = true, data = address };
    }

    public async Task<ServiceResult<ICollection<Address>>> GetAllAdresses(Guid userId)
    {
        var addresses = await _dbContext.Addresses.Where(a => a.UserId == userId && a.DeletedAt == null).ToListAsync();
        if (addresses == null)
            return new ServiceResult<ICollection<Address>>
                { success = false, errorMessage = "User has no Adresses Please Add one" };
        return new ServiceResult<ICollection<Address>> { success = true, data = addresses };
    }
}