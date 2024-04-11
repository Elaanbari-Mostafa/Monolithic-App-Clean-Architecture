namespace Presentation.Contracts.Products;

public sealed record CreateProductRequest(
                                string Name,
                                string Description,
                                string Currency,
                                decimal Price,
                                Guid BrandId,
                                int StockQuantity,
                                bool IsActive);