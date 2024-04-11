using Application.Abstractions.Messaging;

namespace Application.Products.CreateProduct;

public sealed record CreateProductCommand(
                                string Name,
                                string Description,
                                string Currency,
                                decimal Price,
                                Guid BrandId,
                                int StockQuantity,
                                bool IsActive) : ICommand<Guid>;