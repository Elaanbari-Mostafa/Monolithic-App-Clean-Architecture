using Domain.Errors;
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
       => Result.Create(price)
            .Ensure(p => p < 0,DomainErrors.ValueObject.Money.ThePriceIsNegatif)
            .Map(p => new Money(p, currency));

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return new Money(Price, Currency);
    }
}