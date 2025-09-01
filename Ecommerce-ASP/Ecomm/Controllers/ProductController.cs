using Ecomm.models.DTO;
using Ecomm.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecomm.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : Controller
{
    private readonly ProductService _productService;

    public ProductController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var result = await _productService.GetProducts();
        if (result.success) return Ok(result);
        return NotFound(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] CreateProductDTO productDto)
    {
        var result = await _productService.Create(productDto);
        if (result.success) return Ok(result);
        return BadRequest(result);
    }
}