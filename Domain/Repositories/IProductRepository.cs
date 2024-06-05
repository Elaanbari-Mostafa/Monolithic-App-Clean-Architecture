using Domain.Entities;

namespace Domain.Repositories;

public interface IProductRepository : IRepository
{
    void AddProduct(Product product);
    Task<Product?> GetProductByIdAsync(Guid id,CancellationToken cancellationToken = default);
}