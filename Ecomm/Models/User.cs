using System.ComponentModel.DataAnnotations;
using Ecomm.enums;

namespace Ecomm.Models;

public class User
{
    [Key] public Guid id { get; set; } = Guid.NewGuid();

    public string firstName { get; set; }
    public string lastName { get; set; }

    public string username { get; set; }

    public string email { get; set; }
    public string password { get; set; }

    public DateTime birthDate { get; set; }

    public string phoneNumber { get; set; }

    public bool isActive { get; set; } = false; /*Mos harro qeta me bo  default 0 ok!*/

    public Role role { get; set; } = Role.Customer;

    public DateTime createdAt { get; set; } = DateTime.Now;

    public DateTime? updatedAt { get; set; }

    public DateTime? deletedAt { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual Cart Cart { get; set; }
}