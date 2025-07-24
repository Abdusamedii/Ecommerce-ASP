using Ecomm.Data;
using Ecomm.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecomm.Controllers;


[Route("api/[controller]")]
[ApiController]
public class UserController : Controller
{
    private readonly DatabaseConnection _DbContext;

    public UserController(DatabaseConnection db)
    {
        _DbContext = db;
    }
    
    [HttpGet]
    public List<User> Index()
    {
        return _DbContext.Users.ToList();
    }
    [HttpGet("{id}")]
    public IActionResult GetUser(Guid id)
    {
        var user = _DbContext.Users.FirstOrDefault(u => u.id == id);
        return Ok(user);
    }
    [HttpGet("byName")]
    public IActionResult GetUserByName(String name)
    {
        var user = _DbContext.Users.FirstOrDefault(u => u.username == name);
        return Ok(user);
    }

    [HttpPost]
    public IActionResult Post([FromBody] User user)
    {
        _DbContext.Users.Add(user);
        _DbContext.SaveChanges();
        return Ok(user);
    }
    
}

//findByName(String name);
// localhost/api/User/1