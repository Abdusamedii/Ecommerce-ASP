using Ecomm.Data;
using Ecomm.DTO;
using Ecomm.Models;
using Ecomm.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Ecomm.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AdressController : Controller
{
    private readonly AdressService _adressService;

    public AdressController(AdressService adressService)
    {
        _adressService = adressService;
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateAdressDTO adress)
    {
        if (adress is null)
        {
            return BadRequest();
        }

        var response = await  _adressService.CreateAdress(adress);
        return Ok(response);
        /*Mos harro mavon me bo qe as UserID prej DTO mos me marr po prej JWT*/

    }
}