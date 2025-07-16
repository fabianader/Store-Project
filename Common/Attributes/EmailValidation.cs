using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace StoreProject.Common.Attributes
{
    public class EmailValidation : ValidationAttribute
    {
        private const string Pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var email = value.ToString();
                // Check if email matches the pattern
                if (!Regex.IsMatch(email, Pattern))
                {
                    return new ValidationResult("Invalid email format.");
                }

                // Additional checks for specific domain parts or other criteria
                var parts = email.Split('@');
                if (parts.Length != 2 || parts[1].Split('.').Length < 2)
                {
                    return new ValidationResult("Invalid email format.");
                }

                return ValidationResult.Success;
            }
            return new ValidationResult("Email is required.");
        }
    }
}
