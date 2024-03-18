using Domain.Errors;
using Domain.Primitives;
using Domain.Shared;

namespace Domain.ValueObjects;

public sealed class FirstName : ValueObject
{
    public const int MaxLength = 50;
    public string Value { get; private init; }

    private FirstName(string value) => Value = value;

    public static Result<FirstName> Create(string value)
        => Result.Create(value)
                     .Ensure(string.IsNullOrWhiteSpace, DomainErrors.ValueObject.FirstName.Empty)
                     .Ensure(p => p.Length > MaxLength, DomainErrors.ValueObject.FirstName.TooLong)
                     .Map(p => new FirstName(p));

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}