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
        var response = await  _adressService.CreateAdress(adress);
        if (response.success)
        {
        return Ok(response);
                        
        }
        return BadRequest(response);
        /*Mos harro mavon me bo qe as UserID prej DTO mos me marr po prej JWT*/

    }
}