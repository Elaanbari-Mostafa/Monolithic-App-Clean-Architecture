using Domain.Dtos.Products;

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
        IJwtProvider jwtProvider)
    {
        _orderRepository = ThrowIfNull(orderRepository);
        _unitOfWork =    ThrowIfNull(unitOfWork);
        _productRepository = ThrowIfNull(productRepository);
        _jwtProvider = ThrowIfNull(jwtProvider);
    }

    public async Task<Result<Guid>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        Result<Guid> userResult = _jwtProvider.GetUserId();
        if (userResult.IsFailure)
        {
            return Result.Failure<Guid>(userResult.Error);
        }

        IList<ProductIdPriceDto> products = await _productRepository.GetByIdsDtoAsync<ProductIdPriceDto>(request.ProductIds);
        IList<Guid> missingProductIds = request.ProductIds.Except(products.Select(x => x.Id)).ToList();
        if (missingProductIds.Any())
        {
            return Result.Failure<Guid>(DomainErrors.Product.ProductIdsNotFound(missingProductIds));
        }

        Order newOrder = Order.Create(userResult.Value);
        newOrder.AddMany(products);

        _orderRepository.AddOrder(newOrder);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return newOrder.Id;
    }
}
