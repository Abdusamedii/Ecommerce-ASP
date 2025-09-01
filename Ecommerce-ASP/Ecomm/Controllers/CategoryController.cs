using Ecomm.DTO;
using Ecomm.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecomm.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class CategoryController : Controller
{
    public readonly CategoryService _categoryService;

    public CategoryController(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        var result = await _categoryService.getAllCategories();
        /*For loop here to loop through each category and get their Subcategories by parentId*/
        if (result.success) return Ok(result);
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDTO categoryDto)
    {
        var result = await _categoryService.CreateCategory(categoryDto);
        if (result.success) return Ok(result);
        return NotFound(result);
    }

    [HttpPost("SubCategory")]
    public async Task<IActionResult> CreateSubCategory([FromBody] CreateSubCategoryDTO subCategoryDto)
    {
        var result = await _categoryService.CreateSubCategory(subCategoryDto);
        if (result.success) return Ok(result);
        return NotFound(result);
    }
}