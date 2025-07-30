using System.Text.Json.Serialization;

namespace Ecomm.Models;

public class Product
{
    public int id { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public string summary { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public float price { get; set; }
    public int quantity { get; set; }

    [JsonIgnore] public virtual ICollection<CartItem> cartItems { get; set; } = new List<CartItem>();
    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
    public virtual ICollection<ProductSubCategory> ProductCategories { get; set; } = new List<ProductSubCategory>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}