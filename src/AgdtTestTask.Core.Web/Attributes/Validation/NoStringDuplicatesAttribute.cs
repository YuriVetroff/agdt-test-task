using System.ComponentModel.DataAnnotations;

namespace AgdtTestTask.Core.Web.Attributes.Validation
{
    public class NoStringDuplicatesAttribute
        : ValidationAttribute
    {
        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            if (value is IEnumerable<string> items)
            {
                var duplicates = items
                    .GroupBy(x => x, StringComparer.OrdinalIgnoreCase)
                    .Where(g => g.Count() > 1)
                    .Select(g => g.Key)
                    .ToList();
                
                if (duplicates.Any())
                {
                    return new ValidationResult(
                        $"Duplicate given names are not allowed: {string.Join(", ", duplicates)}",
                        ["Name.Given"]);
                }

                return ValidationResult.Success;
            }

            return new ValidationResult("Invalid format");
        }
    }
}
