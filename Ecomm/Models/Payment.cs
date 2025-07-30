using Ecomm.enums;

namespace Ecomm.Models;

public class Payment
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public float Amount { get; set; }
    public string Status { get; set; }
    public Providers Provider { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}