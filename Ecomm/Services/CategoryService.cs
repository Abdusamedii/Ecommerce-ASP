using Ecomm.Data;
using Ecomm.DTO;
using Ecomm.Exceptions;
using Ecomm.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecomm.Services;

public class CategoryService
{
    private readonly DatabaseConnection _dbContext;

    public CategoryService(DatabaseConnection db)
    {
        _dbContext = db;
    }

    public async Task<ServiceResult<Category>> CreateCategory(CreateCategoryDTO categoryDto)
    {
        var category = new Category { Name = categoryDto.Name, Description = categoryDto.Description };
        try
        {
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();
            return new ServiceResult<Category> { success = true, data = category };
        }
        catch (Exception e)
        {
            return new ServiceResult<Category> { success = false, errorMessage = e.Message };
        }
    }

    public async Task<ServiceResult<SubCategory>> CreateSubCategory(CreateSubCategoryDTO subCategoryDto)
    {
        var DoesParentCategoryExist =
            await _dbContext.Categories.AsNoTracking().AnyAsync(c => c.Id == subCategoryDto.parentId);
        if (!DoesParentCategoryExist)
            return new ServiceResult<SubCategory> { success = false, errorMessage = "Parent Category does not exist" };
        var subCategory = new SubCategory
        {
            Name = subCategoryDto.Name, Description = subCategoryDto.Description, ParentId = subCategoryDto.parentId
        };
        try
        {
            await _dbContext.SubCategories.AddAsync(subCategory);
            await _dbContext.SaveChangesAsync();
            return new ServiceResult<SubCategory> { success = true, data = subCategory };
        }
        catch (Exception e)
        {
            return new ServiceResult<SubCategory> { success = false, errorMessage = e.Message };
        }
    }

    public async Task<ServiceResult<List<Category>>> getAllCategories()
    {
        var Categories = await _dbContext.Categories.Include(c => c.SubCategories).ToListAsync();
        return new ServiceResult<List<Category>> { success = true, data = Categories };
    }
}