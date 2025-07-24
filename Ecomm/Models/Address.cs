using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecomm.Models;

public class Address
{
    [Key] public int Id { get; set; }

    [ForeignKey("User")] public Guid UserId { get; set; }

    public string Title { get; set; }
    public string AddressLine1 { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public string? Landmark { get; set; }
    public string PhoneNumber { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? DeletedAt { get; set; }

    public virtual User User { get; set; } = null!;
}