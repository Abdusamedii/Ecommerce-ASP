using System.ComponentModel.DataAnnotations;

namespace Ecomm.DTO;

public class CreateAdressDTO
{
    public Guid UserId { get; set; } /*Mos harro qeta ki me ba find By JWT remove later!!!*/
    [MinLength(4)]
    public string Title { get; set; }
    [MinLength(4)]
    public string AddressLine1 { get; set; }
    [MinLength(4)]
    public string Country { get; set; }
    [MinLength(4)]
    public string City { get; set; }
    [MinLength(4)]
    public string PostalCode { get; set; }
    public string Landmark { get; set; }
    [MinLength(4)]
    public string PhoneNumber { get; set; }
}