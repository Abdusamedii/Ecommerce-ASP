using Ecomm.Data;
using Ecomm.enums;
using Ecomm.Exceptions;
using Ecomm.Models;
using Ecomm.Models.DTO;
using Hangfire;
using Microsoft.EntityFrameworkCore;

namespace Ecomm.Services;

/*In orderService it will be handled the Payment OrderItem and Order by itself*/
public class OrderService
{
    private readonly DatabaseConnection _dbContext;

    public OrderService(DatabaseConnection db)
    {
        _dbContext = db;
    }


    public async Task<ServiceResult<Order>> CreateOrder(CreateOrderDTO orderDtoDto, Guid userId)
    {
        var adress = await _dbContext.Addresses.AsNoTracking().FirstOrDefaultAsync(x => x.Id == orderDtoDto.AdressId);
        if (adress == null)
            return new ServiceResult<Order> { success = false, errorMessage = "This adress does not exist" };


        if (adress.UserId != userId)
            return new ServiceResult<Order> { success = false, errorMessage = "User does not have this adress" };

        var cart = await _dbContext.Carts.AsNoTracking().FirstOrDefaultAsync(c => c.UserId == userId);
        if (cart == null) return new ServiceResult<Order> { success = false, errorMessage = "Cart does not exist" };
        var cartItems = await _dbContext.CartItems.Where(c => c.CartId == cart.Id).ToListAsync();
        if (cartItems.Count <= 0)
            return new ServiceResult<Order> { success = false, errorMessage = "Cart items are empty" };
        var totalAmount = 0f;
        using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            var o = new Order
            {
                AdressId = orderDtoDto.AdressId
            };
            o.CreatedAt = DateTime.Now;
            o.UpdatedAt = DateTime.Now;
            await _dbContext.Orders.AddAsync(o);
            await _dbContext.SaveChangesAsync();
            foreach (var cartItem in cartItems)
            {
                /*Qeky kod ko me ba lock produktin  pasi qe ko me ja ba decrease quantity e me ja jap OrderItem qe tani
                 mos me mujt dy veta perniher me ba ket sken*/
                var product = await _dbContext.Products
                    .FromSqlInterpolated(
                        $"SELECT * FROM Products WITH (UPDLOCK, ROWLOCK) WHERE Id = {cartItem.ProductId}")
                    .FirstOrDefaultAsync();
                if (product == null) throw new Exception("This product does not exist");

                if (product.quantity < cartItem.Quantity) throw new Exception("Not enough Quantity");

                totalAmount += product.price * cartItem.Quantity;

                var oi = new OrderItem
                {
                    OrderId = o.Id,
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                product.quantity -= cartItem.Quantity;


                // BackgroundJob.Schedule()
                await _dbContext.OrderItems.AddAsync(oi);
                _dbContext.CartItems.Remove(cartItem);
            }

            var payment = new Payment
            {
                Amount = totalAmount,
                OrderId = o.Id,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Provider = Providers.Card,
                Status = PaymentStatus.InProgress
            };
            _dbContext.Payments.Add(payment);
            BackgroundJob.Schedule(() => CancelOrderByHangfire(payment.Id), TimeSpan.FromMinutes(1));
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            return new ServiceResult<Order> { success = false, errorMessage = e.Message };
        }

        return new ServiceResult<Order> { success = true };
    }

    public async Task<ServiceResult<ICollection<Order>>> FindOrders(Guid userId)
    {
        var userAddressIds = await _dbContext.Addresses
            .Where(a => a.UserId == userId)
            .Select(a => a.Id)
            .ToListAsync();

        if (!userAddressIds.Any())
            return new ServiceResult<ICollection<Order>>
            {
                success = false,
                errorMessage = "No Orders found for this user"
            };


        var orders = await _dbContext.Orders
            .Where(o => userAddressIds.Contains(o.AdressId)).Include(o => o.OrderItems)
            .ToListAsync();
        if (orders == null)
            return new ServiceResult<ICollection<Order>>
                { success = false, errorMessage = "No Orders found from this account" };

        return new ServiceResult<ICollection<Order>>
        {
            success = true,
            data = orders
        };
    }

    private async Task CancelOrderByHangfire(int paymentId)
    {
        var payment = await _dbContext.Payments
            .Include(p => p.Order)
            .ThenInclude(o => o.OrderItems)
            .FirstOrDefaultAsync(p => p.Id == paymentId);
        if (payment.Status != PaymentStatus.InProgress)
        {
            return;
        }
        payment.Status = PaymentStatus.Cancelled;
        
        foreach (var item in payment.Order.OrderItems)
        {
            var product = await _dbContext.Products.FindAsync(item.ProductId);
            product.quantity += item.Quantity;
        }
        
    }
}