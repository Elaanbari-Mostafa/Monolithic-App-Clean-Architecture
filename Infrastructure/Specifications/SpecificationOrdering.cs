using System.Linq.Expressions;

namespace Infrastructure.Specifications;

internal class SpecificationOrdering<TEntity>
{
    public List<Expression<Func<TEntity, object>>> Ordering { get; } = new();

    public SpecificationOrdering(Expression<Func<TEntity, object>> orderFunction)
        => Ordering.Add(orderFunction);

    public SpecificationOrdering<TEntity> ThenBy(Expression<Func<TEntity, object>> orderFunction)
    {
        Ordering.Add(orderFunction);
        return this;
    }
}