using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Validatiors;

public class MinimumAllowedYearAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        // get the user input 
        var userInputYear = ((DateTime)value).Year;
        if (userInputYear < 1900)
        {
            return new ValidationResult("Year should be no less than 1900");
        }
        return ValidationResult.Success;
    }
}