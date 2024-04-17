namespace Infrastructure.Specifications;

internal sealed class SpecificationOrdering<TEntity>
{
    private readonly List<Expression<Func<TEntity, object>>> _ordering = new();
    public IReadOnlyList<Expression<Func<TEntity, object>>> Ordering => _ordering;
    public bool IsAsc { get; private init; } = true;

    public SpecificationOrdering(Expression<Func<TEntity, object>> orderFunction, bool isAsc = true)
    {
        _ordering.Add(orderFunction);
        IsAsc = isAsc;
    }

    public SpecificationOrdering<TEntity> ThenBy(Expression<Func<TEntity, object>> orderFunction)
    {
        _ordering.Add(orderFunction);
        return this;
    }
}