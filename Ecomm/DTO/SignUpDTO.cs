using System.ComponentModel.DataAnnotations;
using Ecomm.Validation;

namespace Ecomm.DTO;

public class SignUpDTO
{
    [Required,MinLength(6)]
    public string username { get; set; }
    [MinLength(3)]
    public string firstName { get; set; }
    [MinLength(3)]
    public string lastName { get; set; }
    [EmailAddress]
    public string email { get; set; }
    [Phone]
    public string phoneNumber { get; set; }
    [MinLength(8)]
    public string password { get; set; }
    [Compare("password", ErrorMessage = "The password and confirmation password do not match.")]
    public string confirmPassword { get; set; }
    
    [AgeValidation(18, ErrorMessage = "Age must be above 18 years old")]
    public DateTime birthDate { get; set; }
    
}