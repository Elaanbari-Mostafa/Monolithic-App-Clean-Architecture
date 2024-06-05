using Domain.Dtos.Products;

namespace Application.Products.UpdateProduct;

public sealed class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductRepository _productRepository;
    private readonly IBrandRepository _brandRepository;

    public UpdateProductCommandHandler(
      IUnitOfWork unitOfWork,
      IProductRepository productRepository,
      IBrandRepository brandRepository)
          => (_unitOfWork, _productRepository, _brandRepository)
          = (ThrowIfNull(unitOfWork), ThrowIfNull(productRepository), ThrowIfNull(brandRepository));

    public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        Result<VerifyFields> result = await VerifyAsync(
                        request,
                        cancellationToken);
        if (result.IsFailure)
        {
            return result.Error;
        }

        UpdateProductDto updateProduct = request.UpdateProduct;
        result.Value.Product.Update(
                updateProduct.Name,
                updateProduct.Description,
                updateProduct.StockQuantity,
                result.Value.Money,
                result.Value.Brand,
                updateProduct.IsActive);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }

    private async Task<Result<VerifyFields>> VerifyAsync(
        UpdateProductCommand request,
        CancellationToken cancellationToken)
    {
        Product? product = await _productRepository.GetProductByIdAsync(request.UpdateProduct.Id, cancellationToken);
        if (product is null)
        {
            return Result.Failure<VerifyFields>(DomainErrors.Product.ProductNotFound(request.UpdateProduct.Id));
        }

        Brand? brand = await _brandRepository.GetBrandByIdAsync(request.UpdateProduct.BrandId, cancellationToken);
        if (brand is null)
        {
            return Result.Failure<VerifyFields>(DomainErrors.Brand.BrandNotFound(request.UpdateProduct.Id));
        }

        var moneyResult = Money.Create(request.UpdateProduct.Price, request.UpdateProduct.Currency);
        if (moneyResult.IsFailure)
        {
            return Result.Failure<VerifyFields>(moneyResult.Error);
        }

        return new VerifyFields(product, brand, moneyResult.Value);
    }

    private sealed record VerifyFields(Product Product, Brand Brand, Money Money);
}
