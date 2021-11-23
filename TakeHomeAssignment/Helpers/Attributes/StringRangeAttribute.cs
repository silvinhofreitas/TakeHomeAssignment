using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TakeHomeAssignment.Helpers.Attributes
{
    public class StringRangeAttribute : ValidationAttribute
    {
        public string[] ValidValues { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (ValidValues?.Contains(value?.ToString()) == true)
                return ValidationResult.Success;

            var msg = $"Please enter one of the valid values: {string.Join(", ", ValidValues ?? new[] { "No valid values found" })}.";
            return new ValidationResult(msg);
        }
    }

}