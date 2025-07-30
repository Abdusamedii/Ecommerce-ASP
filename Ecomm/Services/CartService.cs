using Ecomm.Data;
using Ecomm.Exceptions;
using Ecomm.Models;
using Ecomm.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace Ecomm.Services;

/*QET CODE E KOM BA VETEN REPEAK PAK SI SHUM SE SE KOM PRIT DUHET ME PERMIRSU TYY*/
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
        var DoesCartItemExist =
            await _dbContext.CartItems.FirstOrDefaultAsync(c =>
                c.ProductId == cartItemDTO.ProductId && c.CartId == cart.Id);
        if (DoesCartItemExist != null)
        {
            /*Qe nga tash e perdori Metoden .HasValue se se pasna dit qe ekziston LMAO*/
            if (DoesCartItemExist.DeletedAt.HasValue)
            {
                DoesCartItemExist.DeletedAt = null;
                DoesCartItemExist.Quantity = 1;
                await _dbContext.SaveChangesAsync();
                return new ServiceResult<CartItem> { success = true, data = DoesCartItemExist };
            }

            var sumQuantity = cartItemDTO.Quantity + DoesCartItemExist.Quantity;
            if (sumQuantity > product.quantity)
                return new ServiceResult<CartItem> { success = false, errorMessage = "Not enough quantity" };
            DoesCartItemExist.Quantity = sumQuantity;
            DoesCartItemExist.UpdatedAt = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();
            return new ServiceResult<CartItem> { success = true, data = DoesCartItemExist };
        }

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

    public async Task<ServiceResult<ICollection<CartItem>>> GetCartItems(Guid? userId)
    {
        if (userId == null)
            return new ServiceResult<ICollection<CartItem>>
                { success = false, errorMessage = "User Id Is null check JWT" };
        var cart = await _dbContext.Carts
            .AsNoTracking()
            .Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.UserId == userId);
        if (cart == null)
            return new ServiceResult<ICollection<CartItem>>
                { success = false, errorMessage = "No cart found with this UserId" };
        return new ServiceResult<ICollection<CartItem>> { success = true, data = cart.CartItems };
    }

    public async Task<ServiceResult<CartItem>> DecrementCartById(UpdateDeleteCartItemDTO cartItemDTO, Guid? userId)
    {
        if (userId == null)
            return new ServiceResult<CartItem> { success = false, errorMessage = "User Id Is null check JWT" };
        var cart = await _dbContext.Carts.AsNoTracking().FirstOrDefaultAsync(c => c.UserId == userId);
        if (cart == null)
            return new ServiceResult<CartItem>
                { success = false, errorMessage = "This cart does not exist please Contact Admin" };
        var cartItem =
            await _dbContext.CartItems.FirstOrDefaultAsync(c => c.CartId == cart.Id && c.Id == cartItemDTO.itemId);
        if (cartItem == null)
            return new ServiceResult<CartItem> { success = false, errorMessage = "This Item is not on the Cart" };

        cartItem.UpdatedAt = DateTime.UtcNow;
        var changedQuantity = cartItem.Quantity - 1;
        if (changedQuantity <= 0)
        {
            cartItem.Quantity = 0;
            cartItem.DeletedAt = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();
            return new ServiceResult<CartItem> { success = true, data = cartItem };
        }

        cartItem.Quantity -= 1;
        await _dbContext.SaveChangesAsync();
        return new ServiceResult<CartItem> { success = true, data = cartItem };
    }

    public async Task<ServiceResult<CartItem>> DeleteCartById(UpdateDeleteCartItemDTO cartItemDTO, Guid? userId)
    {
        if (userId == null)
            return new ServiceResult<CartItem> { success = false, errorMessage = "User Id Is null check JWT" };
        var cart = await _dbContext.Carts.AsNoTracking().FirstOrDefaultAsync(c => c.UserId == userId);
        if (cart == null)
            return new ServiceResult<CartItem>
                { success = false, errorMessage = "This cart does not exist please Contact Admin" };
        var cartItem =
            await _dbContext.CartItems.FirstOrDefaultAsync(c => c.CartId == cart.Id && c.Id == cartItemDTO.itemId);
        if (cartItem == null)
            return new ServiceResult<CartItem> { success = false, errorMessage = "This Item is not on the Cart" };
        cartItem.Quantity = 0;
        cartItem.UpdatedAt = DateTime.UtcNow;
        cartItem.DeletedAt = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync();
        return new ServiceResult<CartItem> { success = true, data = cartItem };
    }
}