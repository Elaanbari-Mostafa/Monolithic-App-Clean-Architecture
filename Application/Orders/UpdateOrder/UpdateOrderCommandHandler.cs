using Application.Abstractions;
using Application.Abstractions.Messaging;
using Domain.Dtos.LineItems;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;
using Domain.ValueObjects;
using Mapster;

namespace Application.Orders.UpdateOrder;

public sealed class UpdateOrderCommandHandler : ICommandHandler<UpdateOrderCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateOrderCommandHandler(
        IOrderRepository orderRepository,
        IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId);
        if (order is null)
        {
            return DomainErrors.Order.OrderNotFound(request.OrderId);
        }

        order.UpdateMany(request.LineItems);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}