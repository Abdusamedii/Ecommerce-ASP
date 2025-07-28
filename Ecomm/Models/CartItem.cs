using System.Text.Json.Serialization;

namespace Ecomm.Models;

public class CartItem
{
    public int Id { get; set; }
    public int CartId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }

    [JsonIgnore] public virtual Cart Cart { get; set; }

    public virtual Product Product { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}