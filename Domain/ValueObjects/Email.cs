using Domain.Errors;
using Domain.Primitives;
using Domain.Shared;

namespace Domain.ValueObjects;

public sealed class Email : ValueObject
{
    public const int MaxLength = 255;
    public string Value { get; private init; }

    private Email(string value) => Value = value;

    public static Result<Email> Create(string value)
        => Result.Create(value)
                     .Ensure(string.IsNullOrWhiteSpace, DomainErrors.ValueObject.Email.Empty)
                     .Ensure(p => p.Length > MaxLength, DomainErrors.ValueObject.Email.TooLong)
                     .Ensure(p => p.Split("@").Length > 1, DomainErrors.ValueObject.Email.InvalidFormat)
                     .Map(p => new Email(p));

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}