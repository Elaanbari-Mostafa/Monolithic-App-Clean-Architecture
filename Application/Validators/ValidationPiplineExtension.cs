namespace Application.Validators;

public static class ValidationPiplineExtension
{
    public static IRuleBuilderOptions<T, TProperty> IsGuid<T, TProperty>(
        this IRuleBuilder<T, TProperty> ruleBuilder)
    => ruleBuilder.SetValidator(new GuidValidator<T, TProperty>());
}