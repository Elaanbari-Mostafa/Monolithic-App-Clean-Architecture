using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Entities;

public sealed class LineItem : Entity, IAuditableEntity
{
    public Guid ProductId { get; private set; }
    public Guid OrderId { get; private set; }
    public Money Money { get; private set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    private LineItem(Guid id) : base(id)
    {

    }

    internal LineItem(Guid id, Guid productId, Guid orderId, Money money) : base(id)
    {
        ProductId = productId;
        OrderId = orderId;
        Money = money;
    }
}