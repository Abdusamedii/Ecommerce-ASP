using Ecomm.Data;
using Ecomm.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecomm.Controllers;


[Route("api/[controller]")]
[ApiController]
public class UserController : Controller
{
    private readonly DatabaseConnection DbContext;

    public UserController(DatabaseConnection db)
    {
        DbContext = db;
    }
    
    [HttpGet]
    public List<User> Index()
    {
        return DbContext.Users.ToList();
    }
    [HttpGet("{id}")]
    public IActionResult GetUser(int id)
    {
        DbContext.Users.FirstOrDefault(u => u.id == id);
        return Ok(DbContext.Users.Find(id));
    }
    [HttpGet("byName")]
    public IActionResult GetUserByName(String name)
    {
        var user = DbContext.Users.FirstOrDefault(u => u.username == name);
        return Ok(user);
    }
    
}

//findByName(String name);
// localhost/api/User/1