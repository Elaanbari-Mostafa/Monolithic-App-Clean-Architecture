using Domain.Primitives;

namespace Domain.Entities;

public sealed class Order : Entity, IAuditableEntity
{
    private readonly IList<LineItem> _lineItems = new List<LineItem>();
    public IReadOnlyList<LineItem> LineItems => _lineItems.AsReadOnly();
    public User User { get; private set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    private Order(Guid id, User user) : base(id)
    {
        User = user;
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