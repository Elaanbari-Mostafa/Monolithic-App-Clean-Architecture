using Domain.Primitives;

namespace Domain.Entities;

public sealed class Order : Entity, IAuditableEntity
{
    private readonly IList<LineItem> _lineItems = new List<LineItem>();
    public IReadOnlyList<LineItem> LineItems => _lineItems.AsReadOnly();
    public User User { get; private set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    private Order(Guid id) : base(id)
    {

    }

    private Order(Guid id, User user) : base(id)
    {
        User = user;
    }

    public static Order Create(User customer)
    {
        var order = new Order(Guid.NewGuid(), customer);
        return order;
    }

    public void Add(Product product)
    {
        var item = new LineItem(Guid.NewGuid(), product.Id, Id, product.Money);
        _lineItems.Add(item);
    }

    public void AddMany(params Product[] products)
    {
        var lineItems = products.Select(product => new LineItem(Guid.NewGuid(), product.Id, Id, product.Money));
        ((List<LineItem>)_lineItems).AddRange(lineItems);
    }

    public void AddMany(IList<Product> products)
    {
        var lineItems = products.Select(product => new LineItem(Guid.NewGuid(), product.Id, Id, product.Money));
        ((List<LineItem>)_lineItems).AddRange(lineItems);
    }
}