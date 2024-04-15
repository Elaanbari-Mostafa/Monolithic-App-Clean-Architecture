using Domain.Entities;
using Domain.Repositories;
using Domain.Shared;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using static Domain.Exceptions.CustomArgumentNullException;

namespace Infrastructure.Repositories;

public sealed class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ProductRepository(ApplicationDbContext dbContext) => _dbContext = ThrowIfNull(dbContext);

    public void AddProduct(Product product)
    {
        _dbContext.Set<Product>().Add(product);
    }

    public async Task<IList<Product>> GetProductByIdsAsync(IEnumerable<Guid> productIds)
    {
        var existingProductIds = await _dbContext.Set<Product>()
            .Where(p => productIds.Contains(p.Id))
            .ToListAsync();

        return existingProductIds;
    }
}