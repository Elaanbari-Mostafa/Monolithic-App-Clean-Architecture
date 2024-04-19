namespace Application.Orders.UpdateOrder;

public sealed class UpdateOrderCommandHandler : ICommandHandler<UpdateOrderCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateOrderCommandHandler(
        IOrderRepository orderRepository,
        IUnitOfWork unitOfWork)
    {
        _orderRepository = ThrowIfNull(orderRepository);
        _unitOfWork = ThrowIfNull(unitOfWork);
    }

    public async Task<Result> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId);
        if (order is null)
        {
            return DomainErrors.Order.OrderNotFound(request.OrderId);
        }

        if (order.OrderStatus != OrderStatus.Pending)
        {
            return DomainErrors.Order.ThisOrderIsNotPending(request.OrderId);
        }

        order.UpdateMany(request.LineItems);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}