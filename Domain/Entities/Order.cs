using Domain.Dtos.LineItems;
using Domain.Dtos.Products;
using Domain.Enums;
using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Entities;

public sealed class Order : Entity, IAuditableEntity
{
    private readonly IList<LineItem> _lineItems = new List<LineItem>();
    public IReadOnlyList<LineItem> LineItems => _lineItems.AsReadOnly();
    public User User { get; private set; }
    public Guid UserId { get; private set; }
    public OrderStatus OrderStatus { get; private set; }
    public Money TotalPrice => CalculateTotalPrice();
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    private Order(Guid id, Guid userId) : base(id)
        => (OrderStatus, UserId) 
        = (OrderStatus.Pending, userId);

    private Money CalculateTotalPrice()
    {
        return Money.Create(
            _lineItems.Sum(item => item.Money.Price),
            _lineItems.First().Money.Currency).Value;
    }

    #region Create Order
    public static Order Create(Guid userId)
    {
        Order order = new(Guid.NewGuid(), userId);
        return order;
    }
    #endregion

    #region Add or update product
    public void Add(ProductIdPriceDto product)
    {
        LineItem item = new(Guid.NewGuid(), product.Id, Id, product.Money, product.Qty);
        _lineItems.Add(item);
    }

    public void UpdateMany(IList<LineItemDto> lineItemsDto)
    {
        var lineItems = from lineItem in lineItemsDto
                        select new LineItem(
                                lineItem.Id,
                                lineItem.ProductId,
                                Id,
                                Money.Create(lineItem.Price, lineItem.Currency).Value,
                                lineItem.Qty);

        ((List<LineItem>)_lineItems).AddRange(lineItems);
    }

    public void AddMany(IEnumerable<ProductIdPriceDto> products)
    {
        IEnumerable<LineItem> lineItems = products
            .Select(product => new LineItem(Guid.NewGuid(), product.Id, Id, product.Money, product.Qty));
        ((List<LineItem>)_lineItems).AddRange(lineItems);
    }
    #endregion

    #region Change order status
    public void MakeAsProcessing() => OrderStatus = OrderStatus.Processing;
    public void MakeAsReturned() => OrderStatus = OrderStatus.Returned;
    public void MakeAsShipped() => OrderStatus = OrderStatus.Shipped;
    public void MakeAsCompleted() => OrderStatus = OrderStatus.Completed;
    #endregion
}