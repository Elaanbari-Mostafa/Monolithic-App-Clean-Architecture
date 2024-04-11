using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Entities;

public sealed class LineItem : Entity, IAuditableEntity
{
    public Product Product { get; private set; }
    public Order Order { get; private set; }
    public Money Price { get; private set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    internal LineItem(Guid id, Product product, Order order, Money price) : base(id)
    {
        Product = product;
        Order = order;
        Price = price;
    }
}
