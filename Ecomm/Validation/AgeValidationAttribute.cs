using System.ComponentModel.DataAnnotations;

namespace Ecomm.Validation;

public class AgeValidationAttribute : ValidationAttribute
{
    private readonly int _minimumAge;

    public AgeValidationAttribute(int minimumAge)
    {
        _minimumAge = minimumAge;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        /*Shikon Object type osht njejt, osht tu prit DateTime*/
        if (value is DateTime birthDate)
        {
            
            /*Data e sotme*/
            var today = DateTime.Today;
            
            
            /*Sorry for the bad code amo vetem shikon qe a je ma i ri sesa 100 vjet Due to reasons reasons*/
            if (birthDate.Date <= (today.AddYears(-100).Date))
            {
                return new  ValidationResult("Age must be Younger than 100 years old");
            }
            /*E hjek vleren e dateLindjes qe eshte jap*/
            int age = today.Year - birthDate.Year;
            /*StackOverflow code amo qka po bojka osht po e shikojka nqoftse ka kalu data e datelindjes , nese po at her nuk e prek moshen nese jo e bon per nja decrement*/
            if (birthDate.Date > today.AddYears(-age)) 
            {
                age--;
            }
            if (!(age >= _minimumAge))
            {
                return new ValidationResult("Age must be more than than 18 years old.");
            }
            return ValidationResult.Success;

        }
        return new ValidationResult("Invalid birth date");
    }
}