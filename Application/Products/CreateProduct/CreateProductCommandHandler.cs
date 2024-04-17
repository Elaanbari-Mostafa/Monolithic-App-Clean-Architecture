namespace Application.Products.CreateProduct;

public sealed class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductRepository _productRepository;
    private readonly IBrandRepository _brandRepository;

    public CreateProductCommandHandler(
        IUnitOfWork unitOfWork,
        IProductRepository productRepository,
        IBrandRepository brandRepository)
            => (_unitOfWork, _productRepository, _brandRepository)
            = (ThrowIfNull(unitOfWork), ThrowIfNull(productRepository), ThrowIfNull(brandRepository));

    public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var (result, brand, money) = await Verify(request, cancellationToken);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        Product product = Product.Create(
                    request.Name,
                    request.Description,
                    money!,
                    brand!,
                    request.StockQuantity,
                    request.IsActive);

        _productRepository.AddProduct(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return product.Id;
    }

    private async Task<(Result Result, Brand? Brand, Money? Money)> Verify(
            CreateProductCommand request,
            CancellationToken cancellationToken)
    {
        var brandById = await _brandRepository.GetBrandByIdAsync(request.BrandId, cancellationToken);
        if (brandById is null)
        {
            return (DomainErrors.Brand.BrandNotFound(request.BrandId), null, null);
        }
        Brand? brand = brandById;

        Result<Money> moneyResult = Money.Create(request.Price, request.Currency);
        if (moneyResult.IsFailure)
        {
            return (moneyResult.Error, null, null);
        }
        Money? money = moneyResult.Value;

        return (Result.Success(), brand, money);
    }
}