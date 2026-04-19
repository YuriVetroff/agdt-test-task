using System.ComponentModel.DataAnnotations;

namespace AgdtTestTask.Core.Web.Attributes.Validation
{
    public class BirthdateAttribute
        : ValidationAttribute
    {
        private readonly DateTime _minimum = new(1900, 1, 1);

        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext)
        {
            if (value is DateTime birthdate)
            {
                if (birthdate < _minimum || birthdate > DateTime.UtcNow)
                {
                    return new ValidationResult(
                        $"Birthdate is out of possible range: {birthdate}",
                        ["Name.Given"]);
                }

                return ValidationResult.Success;
            }

            return new ValidationResult("Invalid format.");
        }
    }
}
