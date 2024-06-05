using Domain.Dtos.Orders;

namespace Application.Payments.CreatePayment;

public sealed class CreatePaymentCommandHandler : ICommandHandler<CreatePaymentCommand, Guid>
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePaymentCommandHandler(
        IPaymentRepository paymentRepository,
        IOrderRepository orderRepository,
        IUnitOfWork unitOfWork)
    => (_paymentRepository, _orderRepository, _unitOfWork)
    = (ThrowIfNull(paymentRepository), ThrowIfNull(orderRepository), ThrowIfNull(unitOfWork));

    public async Task<Result<Guid>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        OrderWithPriceDto? orderWithPrice = await _orderRepository.GetOrderWithTotalPrice(request.OrderId);
        if (orderWithPrice is null)
        {
            return Result.Failure<Guid>(DomainErrors.Order.OrderNotFound(request.OrderId));
        }

        if (!orderWithPrice.Order.OrderStatus.Equals(OrderStatus.Pending))
        {
            return Result.Failure<Guid>(DomainErrors.Order.ThisOrderIsNotPending(request.OrderId));
        }

        Payment payment = Payment.Create(request.Method, orderWithPrice);
        _paymentRepository.AddPayment(payment);
        orderWithPrice.Order.MakeAsProcessing();
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return payment.Id;
    }
}