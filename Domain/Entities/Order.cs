using Domain.Dtos.LineItems;
using Domain.Dtos.Products;
using Domain.Primitives;
using Domain.ValueObjects;
using static Domain.Errors.DomainErrors;

namespace Domain.Entities;

public sealed class Order : Entity, IAuditableEntity
{
    private readonly IList<LineItem> _lineItems = new List<LineItem>();
    public IReadOnlyList<LineItem> LineItems => _lineItems.AsReadOnly();
    public User User { get; private set; }
    public Guid UserId { get; private set; }
    public Money TotalPrice => CalculateTotalPrice();
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    private Order(Guid id) : base(id)
    {

    }

    public static Order Create(User user)
    {
        var order = new Order(Guid.NewGuid(), user);
        return order;
    }

    public void AddItem(LineItem item)
    {
        _lineItems.Add(item);
    }
}