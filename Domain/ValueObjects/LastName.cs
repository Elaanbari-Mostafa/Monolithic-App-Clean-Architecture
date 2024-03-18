using Domain.Errors;
using Domain.Primitives;
using Domain.Shared;

namespace Domain.ValueObjects;

public sealed class LastName : ValueObject
{
    public const int MaxLength = 50;
    public string Value { get; private init; }

    private LastName(string value) => Value = value;

    public static Result<LastName> Create(string value)
        => Result.Create(value)
                     .Ensure(string.IsNullOrWhiteSpace, DomainErrors.ValueObject.LastName.Empty)
                     .Ensure(p => p.Length > MaxLength, DomainErrors.ValueObject.LastName.TooLong)
                     .Map(p => new LastName(p));

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}