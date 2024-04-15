using Domain.Entities;

namespace Domain.Repositories;

public interface IProductRepository
{
    void AddProduct(Product product);
    Task<IList<Product>> GetProductByIdsAsync(IEnumerable<Guid> productIds);
}