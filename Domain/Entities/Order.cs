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

    private Order(Guid id, Guid userId) : base(id)
    {
        UserId = userId;
    }

    public static Order Create(Guid userId)
    {
        Order order = new(Guid.NewGuid(), userId);
        return order;
    }

    public void Add(ProductIdPriceDto product)
    {
        LineItem item = new(Guid.NewGuid(), product.Id, Id, product.Money);
        _lineItems.Add(item);
    }

    public void AddMany(IList<ProductIdPriceDto> products)
    {
        IEnumerable<LineItem> lineItems = products.Select(product => new LineItem(Guid.NewGuid(), product.Id, Id, product.Money));
        ((List<LineItem>)_lineItems).AddRange(lineItems);
    }

    private Money CalculateTotalPrice()
    {
        return Money.Create(_lineItems.Sum(item => item.Money.Price), _lineItems.First().Money.Currency).Value;
    }

    public void UpdateMany(IList<LineItemDto> lineItemsDto)
    {
        IList<LineItem> lineItems = lineItemsDto.Select(x => new LineItem(
                x.Id,
                x.ProductId,
                Id,
                Money.Create(x.Price, x.Currency).Value)).ToList();

        ((List<LineItem>)_lineItems).AddRange(lineItems);
    }
}