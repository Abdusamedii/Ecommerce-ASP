using CalConnect.Api.Users.Infrastructure;
using Ecomm.Exceptions;
using Ecomm.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecomm.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : Controller
{
    private readonly TokenProvider _tokenProvider;
    private readonly UserService _userService;

    public UserController(UserService userService, TokenProvider tokenProvider)
    {
        _userService = userService;
        _tokenProvider = tokenProvider;
    }

    [HttpGet("getUserFromJwt")]
    public async Task<IActionResult> GetUserFromJwt()
    {
        var username = _tokenProvider.GetUsernameByJwt(HttpContext);
        if (username == null)
            return NotFound(new ServiceResult<string>
                { success = false, data = "JWT is unable to extract the username" });
        var user = await _userService.findUser(username);
        if (user.success) return Ok(user);
        return NotFound(new ServiceResult<string> { success = false, data = "JWT is unable to extract the user" });
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllUsers()
    {
        var result = await _userService.findAll();
        if (result.success) return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("{username}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetByUsername(string username)
    {
        var result = await _userService.findUser(username);
        if (result.success) return Ok(result);
        return BadRequest(result);
    }
}

//findByName(String name);
// localhost/api/User/1