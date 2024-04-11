using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Entities;

public sealed class Product : Entity, IAuditableEntity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public Money Money { get; private set; }
    public string Category { get; private set; }
    public int StockQuantity { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
    public ProductBrand ProductBrand { get; private set; }

    private Product(
        Guid id,
        string name,
        string description,
        Money money,
        string category,
        ProductBrand productBrand,
        int stockQuantity,
        bool isActive) : base(id)
     => (Name, Description, Money, Category, ProductBrand, StockQuantity, IsActive)
      = (name, description, money, category, productBrand, stockQuantity, isActive);

    public static Product Create(
        string name,
        string description,
        Money money,
        string category,
        ProductBrand productBrand,
        int stockQuantity,
        bool isActive)
     => new(Guid.NewGuid(), name, description, money, category, productBrand, stockQuantity, isActive);

}