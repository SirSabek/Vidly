using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Min18YearsIfAMember : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var customer = validationContext.ObjectInstance as Customer;

            if (customer == null)
            {
                return ValidationResult.Success;
            }

            if (customer.MembershipTypeId == MembershipType.Unknown || customer.MembershipTypeId == MembershipType.PayAsYouGo)
            {
                return ValidationResult.Success;
            }

            if (customer.BirthDate == null)
            {
                return new ValidationResult("Birthdate is required.");
            }

            var birthDate = customer.BirthDate.Value;
            var age = DateTime.Today.Year - birthDate.Year;

            // Adjust age if the birthday hasn't occurred yet this year
            if (birthDate > DateTime.Today.AddYears(-age))
            {
                age--;
            }

            return age >= 18 
                ? ValidationResult.Success 
                : new ValidationResult("Customer should be at least 18 years old to go on a membership.");
        }
    }
}
