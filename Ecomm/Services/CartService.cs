using Ecomm.Data;
using Ecomm.Exceptions;
using Ecomm.Models;
using Ecomm.Models.DTO;
using Microsoft.EntityFrameworkCore;

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

    public async Task<ServiceResult<CartItem>> AddToCart(CreateCartItemDTO cartItemDTO, Guid? userId)
    {
        var cart = await _dbContext.Carts.AsNoTracking().FirstOrDefaultAsync(c => c.UserId == userId);
        if (cart == null)
            return new ServiceResult<CartItem>
                { success = false, errorMessage = "This user does not have a Cart please contact Admin" };
        var product = await _dbContext.Products.FindAsync(cartItemDTO.ProductId);
        if (product == null)
            return new ServiceResult<CartItem> { success = false, errorMessage = "This Product does not exist" };

        if (product.quantity < cartItemDTO.Quantity)
            return new ServiceResult<CartItem> { success = false, errorMessage = "Not enough quantity" };
        /*Qetu e bon logjken nese e ka ba add to cart apet itemin e njejt at her e kqyr nese quantity i tanishem plus quantity
         qe po don me u shtu osht ma i nalt sesa quantity total, gjithashtu e ban update tani CartItem Quantity se osht tum zan gjumi
         ZZZZZZZZZZZZZZZZZZZZZZZZz
         SLEEEEEP
         ZZZZZZZZZZZZZZZZZZZZZZZZZ
         Fale Sabahun ora 2:30
         */
        var cartItem = new CartItem
        {
            CartId = cart.Id,
            ProductId = cartItemDTO.ProductId,
            Quantity = cartItemDTO.Quantity
        };
        try
        {
            await _dbContext.CartItems.AddAsync(cartItem);
            await _dbContext.SaveChangesAsync();
            return new ServiceResult<CartItem> { success = true, data = cartItem };
        }
        catch (Exception ex)
        {
            return new ServiceResult<CartItem> { success = false, errorMessage = ex.Message };
        }
    }
}