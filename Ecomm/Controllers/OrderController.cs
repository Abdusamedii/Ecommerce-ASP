using CalConnect.Api.Users.Infrastructure;
using Ecomm.Models.DTO;
using Ecomm.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecomm.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : Controller
{
    private readonly OrderService _orderService;
    private readonly TokenProvider _tokenProvider;

    public OrderController(OrderService orderService, TokenProvider tokenProvider)
    {
        _orderService = orderService;
        _tokenProvider = tokenProvider;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDTO createOrderDtoDto)
    {
        var userId = _tokenProvider.GetIdByJwt(HttpContext);
        if (!userId.HasValue) return Unauthorized();
        var result = await _orderService.CreateOrder(createOrderDtoDto, userId.Value);
        if (result.success) return Ok(result);
        return BadRequest(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        var userId = _tokenProvider.GetIdByJwt(HttpContext);
        if (!userId.HasValue) return Unauthorized();
        var result = await _orderService.FindOrders(userId.Value);
        if (result.success) return Ok(result);
        return BadRequest(result);
    }
}