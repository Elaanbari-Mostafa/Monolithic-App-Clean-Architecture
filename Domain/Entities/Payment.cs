using Domain.Dtos.Orders;
using Domain.Enums;
using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Entities;

public sealed class Payment : Entity, IAuditableEntity
{
    public Money Amount { get; private set; }
    public DateTime PaymentDate { get; private set; }
    public PaymentStatus Status { get; private set; }
    public PaymentMethod Method { get; private set; }
    public Order Order { get; private set; }
    public Guid OrderId { get; private set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    private Payment(Guid id) : base(id) { }

    private Payment(Guid id, Money amount, PaymentMethod method, Guid orderId) : base(id)
    {
        Amount = amount;
        PaymentDate = DateTime.UtcNow;
        Status = PaymentStatus.Pending;
        Method = method;
        OrderId = orderId;
    }

    public static Payment Create(PaymentMethod method, OrderWithPriceDto orderWithItems)
    {
        Payment payment = new(Guid.NewGuid(), orderWithItems.Money, method, orderWithItems.Order.Id);
        return payment;
    }

    public void MarkAsCompleted()
    {
        Status = PaymentStatus.Completed;
    }

    public void MarkAsFailed()
    {
        Status = PaymentStatus.Failed;
    }
}