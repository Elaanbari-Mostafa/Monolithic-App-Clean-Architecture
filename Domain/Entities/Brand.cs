using Domain.Primitives;

namespace Domain.Entities;

public sealed class Brand : Entity, IAuditableEntity
{
    public string Name { get; private set; }
    private readonly IList<Product> _products = new List<Product>();
    public IReadOnlyList<Product> Products => _products.AsReadOnly();
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    private Brand(Guid id, string name) : base(id)
        => (Name) = (name);

    public static Brand Create(string name)
        => new(Guid.NewGuid(), name);
}