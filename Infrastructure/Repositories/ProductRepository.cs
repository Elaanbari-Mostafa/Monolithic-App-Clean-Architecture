using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data;
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
}