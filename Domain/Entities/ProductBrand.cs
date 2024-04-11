using Domain.Enums;

namespace Domain.Entities;

public sealed class ProductBrand : Enumeration<ProductBrand>
{
    public static readonly ProductBrand None = new(1, nameof(None));
    public static readonly ProductBrand Dell = new(2, nameof(Dell));
    public static readonly ProductBrand HP = new(3, nameof(HP));
    public static readonly ProductBrand Lenovo = new(4, nameof(Lenovo));
    public static readonly ProductBrand Zara = new(5, nameof(Zara));

    public ProductBrand(int id, string name) : base(id, name)
    {
    }
}