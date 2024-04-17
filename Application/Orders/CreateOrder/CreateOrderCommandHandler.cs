using Application.Abstractions;
using Application.Abstractions.Messaging;
using Domain.Dtos.Products;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Orders.CreateOrder;

public sealed class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, Guid>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtProvider _jwtProvider;

    public CreateOrderCommandHandler(
        IOrderRepository orderRepository,
        IUnitOfWork unitOfWork,
        IProductRepository productRepository,
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
    }

    public async Task<Result<Guid>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        {
        }

        List<Guid> missingProductIds = request.ProductIds.Except(products.Select(x => x.Id)).ToList();
        if (missingProductIds.Any())
        {
            return Result.Failure<Guid>(DomainErrors.Product.ProductIdsNotFound(missingProductIds));
        }

        newOrder.AddMany(products);

        _orderRepository.AddOrder(newOrder);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return newOrder.Id;
    }
}
