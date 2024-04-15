using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Orders.CreateOrder;

public sealed class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, Guid>
{
    public readonly IOrderRepository _orderRepository;
    public readonly IProductRepository _productRepository;
    public readonly IUserRepository _userRepository;
    public readonly IUnitOfWork _unitOfWork;

    public CreateOrderCommandHandler(
        IOrderRepository orderRepository,
        IUnitOfWork unitOfWork,
        IProductRepository productRepository,
        IUserRepository userRepository)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
        _userRepository = userRepository;
    }

    public async Task<Result<Guid>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetUserByIdAsync(request.CustomerId, cancellationToken);
        if (user is null)
        {
            return Result.Failure<Guid>(DomainErrors.User.UserWithIdNotFound(request.CustomerId));
        }

        IList<Product> products = await _productRepository.GetProductByIdsAsync(request.ProductIds);
        List<Guid> missingProductIds = request.ProductIds.Except(products.Select(x => x.Id)).ToList();
        if (missingProductIds.Any())
        {
            return Result.Failure<Guid>(DomainErrors.Product.ProductIdsNotFound(missingProductIds));
        }

        Order newOrder = Order.Create(user);
        newOrder.AddMany(products);

        _orderRepository.AddOrder(newOrder);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return newOrder.Id;
    }
}
