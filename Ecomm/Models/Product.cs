namespace Ecomm.Models;

public class Product
{
    public int id { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public string summary { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
    public virtual ICollection<ProductSubCategory> ProductCategories { get; set; } = new List<ProductSubCategory>();
}