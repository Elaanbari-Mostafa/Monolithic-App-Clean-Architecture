using FluentValidation.Validators;

namespace Application.Validators;

internal sealed class GuidValidator<T, TProperty> : PropertyValidator<T, TProperty>
{
    public override string Name => "IsGuidValidator";

    public override bool IsValid(ValidationContext<T> context, TProperty value)
    {
        if (value is null) return false;

        if (value is Guid) return true;

        if (value is string stringValue)
        {
            return Guid.TryParse(stringValue, out _);
        }

        return false;
    }
}