using System.Text.Json.Serialization;

namespace Ecomm.Models;

public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    [JsonIgnore] public virtual Order Order { get; set; }
    public virtual Product Product { get; set; }
}