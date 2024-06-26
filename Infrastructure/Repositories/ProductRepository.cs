﻿namespace Infrastructure.Repositories;

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
        List<Product> existingProductIds = await _dbContext.Set<Product>()
            .Where(p => productIds.Contains(p.Id))
            .ToListAsync();

        return existingProductIds;
    }

    public async Task<IList<T>> GetByIdsDtoAsync<T>(IEnumerable<Guid> productIds)
    {
        var existingProductIds = await _dbContext.Set<Product>()
            .Where(p => productIds.Contains(p.Id))
            .Select(p => p.CreateDto<T>(null))
            .ToListAsync();

        return existingProductIds;
    }

    public async Task<T?> GetByIdDtoAsync<T>(Guid id)
    {
        var product = await _dbContext.Set<Product>()
            .Where(p => p.Id == id)
            .Select(p => p.CreateDto<T>(null))
            .FirstOrDefaultAsync();

        return product;
    }

    public async Task<Product?> GetProductByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Product? product = await _dbContext.Set<Product>()
                .FirstOrDefaultAsync(p => p.Id == id,cancellationToken);
        return product;
    }
}