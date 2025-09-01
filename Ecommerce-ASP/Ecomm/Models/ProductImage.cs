using System.Text.Json.Serialization;

namespace Ecomm.Models;

public class ProductImage
{
    public int Id { get; set; }
    public string ImagePath { get; set; }
    public int ProductId { get; set; }
    [JsonIgnore] public virtual Product Product { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}