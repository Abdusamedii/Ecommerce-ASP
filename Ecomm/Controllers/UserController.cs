using Ecomm.DTO;
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
    public async Task<IActionResult> Index()
    {
        var result = await _userService.findAll();
        if (result.success) return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("{username}")]
    public async Task<IActionResult> Get(string username)
    {
        var result = await _userService.findUser(username);
        if (result.success) return Ok(result);
        return BadRequest(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] SignUpDTO user)
    {
        var result = await _userService.SaveUser(user);
        if (result.success) return Ok(result);
        return BadRequest(result);
    }
}

//findByName(String name);
// localhost/api/User/1