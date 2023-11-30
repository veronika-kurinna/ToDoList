using System.ComponentModel.DataAnnotations;

namespace ToDoList.Dtos.Attributes
{
    public class ValidateNotNullOrNotWhiteSpaceAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value,
                                       ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult("This field is required");
            }

            return ValidationResult.Success;
        }
    }
}
