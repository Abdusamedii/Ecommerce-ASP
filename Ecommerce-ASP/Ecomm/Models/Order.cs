namespace Ecomm.Models;

public class Order
{
    public int Id { get; set; }
    public int AdressId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }


    public virtual Address Address { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; }

    public virtual Payment Payment { get; set; }
}