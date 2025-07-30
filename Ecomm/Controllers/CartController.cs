using CalConnect.Api.Users.Infrastructure;
using Ecomm.Models.DTO;
using Ecomm.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecomm.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartController : Controller
{
    private readonly CartService _CartService;
    private readonly TokenProvider _TokenProvider;

    public CartController(CartService cartService, TokenProvider tokenProvider)
    {
        _CartService = cartService;
        _TokenProvider = tokenProvider;
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart([FromBody] CreateCartItemDTO cartItemDto)
    {
        var userId = _TokenProvider.GetIdByJwt(HttpContext);
        if (userId == null) return Unauthorized();
        var result = await _CartService.AddToCart(cartItemDto, userId);
        if (result.success) return Ok(result);
        return BadRequest(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetCartItems()
    {
        var userId = _TokenProvider.GetIdByJwt(HttpContext);
        if (userId == null) return Unauthorized();
        var result = await _CartService.GetCartItems(userId);
        if (result.success) return Ok(result);
        return BadRequest(result);
    }

    [HttpPut]
    public async Task<IActionResult> DecrementCartItem([FromBody] UpdateDeleteCartItemDTO cartItemDto)
    {
        var userId = _TokenProvider.GetIdByJwt(HttpContext);
        if (userId == null) return Unauthorized();
        var result = await _CartService.DecrementCartById(cartItemDto, userId);
        if (result.success) return Ok(result);
        return BadRequest(result);
    }
}