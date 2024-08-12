using System.ComponentModel.DataAnnotations;

namespace Vidly.Models;

public class Min18YearsIfAMember : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var customer = validationContext.ObjectInstance;
        
        if (customer is not Customer)
        {
            return ValidationResult.Success;
        }
        
        if (((Customer)customer).MembershipTypeId == MembershipType.Unknown || ((Customer)customer).MembershipTypeId == MembershipType.PayAsYouGo)
        {
            return ValidationResult.Success;
        }
        
        if (((Customer)customer).BirthDate == null)
        {
            return new ValidationResult("Birthdate is required.");
        }
        
        var age = DateTime.Today.Year - ((Customer)customer).BirthDate.Value.Year;
        return age >= 18 
            ? ValidationResult.Success 
            : new ValidationResult("Customer should be at least 18 years old to go on a membership.");
    }
}