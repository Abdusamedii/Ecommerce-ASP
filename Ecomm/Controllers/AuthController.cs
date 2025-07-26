using Ecomm.DTO;
using Ecomm.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecomm.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class AuthController : Controller
{
    private readonly AuthService _authService;
    private readonly UserService _userService;

    public AuthController(AuthService authService, UserService userService)
    {
        _authService = authService;
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> SignIn([FromBody] SignInDTO login)
    {
        var response = await _authService.login(login);
        if (response.success) return Ok(response);
        return BadRequest(response);
    }

    [HttpPost("register")]
    public async Task<IActionResult> SignUp([FromBody] SignUpDTO user)
    {
        var result = await _userService.SaveUser(user);
        if (result.success) return Ok(result);

        return BadRequest(result);
    }
}