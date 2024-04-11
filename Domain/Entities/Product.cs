using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Entities;

public sealed class Product : Entity, IAuditableEntity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public Money Money { get; private set; }
    public int StockQuantity { get; private set; }
    public bool IsActive { get; private set; }
    public Brand Brand { get; private set; }
    public Guid BrandId { get; private set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    private Product(Guid id) : base(id)
    {
        
    }

    private Product(
        Guid id,
        string name,
        string description,
        int stockQuantity,
        Money money,
        Brand brand,
        bool isActive) : base(id)
     => (Name, Description, StockQuantity, IsActive, Money, Brand)
      = (name, description, stockQuantity, isActive, money, brand);

    public static Product Create(
        string name,
        string description,
        Money money,
        Brand brand,
        int stockQuantity,
        bool isActive)
    => new(Guid.NewGuid(), name, description, stockQuantity, money, brand, isActive);
}