using Ecomm.Data;
using Ecomm.DTO;
using Ecomm.Models;
using Ecomm.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    public List<User> Index()
    {
        // return _DbContext.Users.ToList();
        return new List<User>();
    }
    [HttpPost]
    public async  Task<IActionResult> Post([FromBody] SignUpDTO user)
    {
        var result = await _userService.SaveUser(user);
        return Ok(result);
    }
    
}

//findByName(String name);
// localhost/api/User/1