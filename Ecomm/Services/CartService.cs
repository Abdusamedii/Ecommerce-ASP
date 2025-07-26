using Ecomm.Data;
using Ecomm.Exceptions;
using Ecomm.Models;

namespace Ecomm.Services;

public class CartService
{
    private readonly DatabaseConnection _dbContext;

    public CartService(DatabaseConnection db)
    {
        _dbContext = db;
    }

    public async Task<ServiceResult<Cart>> CreateCart(Guid userId)
    {
        var newCart = new Cart();
        newCart.UserId = userId;
        var createdCart = await _dbContext.Carts.AddAsync(newCart);
        await _dbContext.SaveChangesAsync();
        return new ServiceResult<Cart> { success = true, data = createdCart.Entity };
    }
}