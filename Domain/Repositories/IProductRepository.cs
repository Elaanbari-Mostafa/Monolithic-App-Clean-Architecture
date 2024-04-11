using Domain.Entities;

namespace Domain.Repositories;

public interface IProductRepository
{
    void AddProduct(Product product);
}
