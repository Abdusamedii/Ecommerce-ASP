using Ecomm.Data;
using Ecomm.Exceptions;
using Ecomm.Models;
using Ecomm.models.DTO;
using Microsoft.EntityFrameworkCore;

namespace Ecomm.Services;

public class ProductService
{
    private readonly DatabaseConnection _dbContext;

    public ProductService(DatabaseConnection db)
    {
        _dbContext = db;
    }

    public async Task<ServiceResult<Product>> Create(CreateProductDTO productDto)
    {
        using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            var product = new Product
            {
                name = productDto.name,
                description = productDto.description,
                summary = productDto.summary
            };
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();


            foreach (var subCategoryId in productDto.subCategoryId)
            {
                var productSubCategory = new ProductSubCategory
                {
                    ProductId = product.id,
                    SubCategoryId = subCategoryId
                };
                await _dbContext.ProductSubCategories.AddAsync(productSubCategory);
            }

            foreach (var imagePath in productDto.ImagePath)
            {
                var image = new ProductImage
                {
                    ProductId = product.id,
                    ImagePath = imagePath
                };
                await _dbContext.ProductImages.AddAsync(image);
            }

            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
            return new ServiceResult<Product> { data = product, success = true };
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();

            return new ServiceResult<Product>
            {
                success = false,
                errorMessage = "Failed to create product with categories. Reason: " + ex.Message
            };
        }

        return null;
    }

    public async Task<ServiceResult<List<Product>>> GetProducts()
    {
        try
        {
            var products = _dbContext.Products.Include(p => p.ProductImages).Include(p => p.ProductCategories).ToList();
            return new ServiceResult<List<Product>> { data = products, success = true };
        }
        catch (Exception ex)
        {
            return new ServiceResult<List<Product>> { success = false, errorMessage = ex.Message };
        }
    }
}