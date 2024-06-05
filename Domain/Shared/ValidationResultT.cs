namespace Domain.Shared;

public sealed class ValidationResult<TResult> : Result<TResult>, IValidationResult
{
    public Error[] Errors { get; }

    private ValidationResult(Error[] errors) : base(default, false, IValidationResult.ValidationError)
        => Errors = errors;

    public static ValidationResult<TResult> WithErrors(Error[] errors) => new(errors);
}