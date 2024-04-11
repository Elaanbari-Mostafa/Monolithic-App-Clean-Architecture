using Domain.Primitives;
using Domain.Shared;

namespace Domain.ValueObjects;

public sealed class Money : ValueObject
{
    public decimal Price { get; private set; }
    public string Currency { get; private set; }

    private Money(decimal price, string currency)
    {
        Price = price;
        Currency = currency;
    }

    public static Result<Money> Create(decimal price, string currency)
    {
        return new Money(price, currency);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return new Money(Price, Currency);
    }
}