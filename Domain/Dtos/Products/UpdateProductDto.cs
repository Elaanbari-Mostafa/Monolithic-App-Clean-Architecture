namespace Domain.Dtos.Products;

public sealed record UpdateProductDto(
                            Guid Id,
                            string Name,
                            string Description,
                            int StockQuantity,
                            decimal Price,
                            string Currency,
                            Guid BrandId,
                            bool IsActive);