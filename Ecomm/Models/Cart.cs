using System.Text.Json.Serialization;

namespace Ecomm.Models;

public class Cart
{
    public int Id { get; set; }
    public Guid UserId { get; set; }

    [JsonIgnore] public virtual User User { get; set; }
    public virtual ICollection<CartItem> CartItems { get; set; }
}