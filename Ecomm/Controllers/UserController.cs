using Ecomm.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecomm.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : Controller
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var result = await _userService.findAll();
        if (result.success) return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("{username}")]
    public async Task<IActionResult> GetByUsername(string username)
    {
        var result = await _userService.findUser(username);
        if (result.success) return Ok(result);
        return BadRequest(result);
    }
}

//findByName(String name);
// localhost/api/User/1