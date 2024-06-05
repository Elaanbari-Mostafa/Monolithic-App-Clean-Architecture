namespace Domain.Shared;

public interface IValidationResult
{
    public static readonly Error ValidationError = new(
        "ValidationError",
        "A validation probleme occurred.");

    Error[] Errors { get; }
}
