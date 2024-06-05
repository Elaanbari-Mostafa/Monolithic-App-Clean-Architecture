using Domain.Dtos.Products;

namespace Application.Products.UpdateProduct;

public sealed record UpdateProductCommand(UpdateProductDto UpdateProduct) : ICommand;