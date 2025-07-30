using CalConnect.Api.Users.Infrastructure;
using Ecomm.DTO;
using Ecomm.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecomm.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdressController : Controller
{
    private readonly AdressService _adressService;
    private readonly TokenProvider _tokenProvider;

    public AdressController(AdressService adressService, TokenProvider tokenProvider)
    {
        _adressService = adressService;
        _tokenProvider = tokenProvider;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateAdressDTO adress)
    {
        var userId = _tokenProvider.GetIdByJwt(HttpContext);
        if (userId == null) return Unauthorized();
        adress.UserId = userId.Value;
        var response = await _adressService.CreateAdress(adress);
        if (response.success) return Ok(response);
        return BadRequest(response);
        /*Mos harro mavon me bo qe as UserID prej DTO mos me marr po prej JWT*/
    }

    [HttpGet]
    public async Task<IActionResult> GetAdress()
    {
        var userId = _tokenProvider.GetIdByJwt(HttpContext);
        if (userId == null) return Unauthorized();
        var result = await _adressService.GetAllAdresses(userId.Value);
        if (result.success) return Ok(result);
        return BadRequest(result);
    }
}