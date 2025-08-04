using Ecomm.enums;

namespace Ecomm.Models.DTO;

public class CreateOrderDTO
{
    public int AdressId { get; set; }

    public Providers Provider { get; set; }
}