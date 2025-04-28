using System.ComponentModel.DataAnnotations;
using System.Reflection;

public class RequiredMessageAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        bool isInvalid = value == null || (value is string str && string.IsNullOrWhiteSpace(str));

        if (!isInvalid)
            return ValidationResult.Success;

        var displayAttr = validationContext
            .ObjectType
            .GetProperty(validationContext.MemberName)
            ?.GetCustomAttribute<DisplayAttribute>();

        string label = displayAttr?.Name ?? validationContext.DisplayName ?? validationContext.MemberName;
        string message = $"O campo '{label}' é obrigatório.";

        return new ValidationResult(message);
    }
}