using Domain.Primitives;
using System.Linq.Expressions;

namespace Infrastructure.Specifications;

internal abstract class Specification<TEntity> where TEntity : Entity
{
    private Expression<Func<TEntity, bool>>? Cretaria { get; }
    private bool _orderByCalled = false;

    protected Specification(Expression<Func<TEntity, bool>>? cretaria)
        => Cretaria = cretaria;

    public List<Expression<Func<TEntity, object>>> Conditions { get; } = new();
    public SpecificationOrdering<TEntity>? SpecificationOrdering { get; private set; }

    protected void AddInclude(Expression<Func<TEntity, object>> condition) => Conditions.Add(condition);
    protected SpecificationOrdering<TEntity> OrderBy(Expression<Func<TEntity, object>> orderFunction)
    {
        if (_orderByCalled)
        {
            throw new InvalidOperationException("AddOrderBy can only be called once.");
        }

        _orderByCalled = true;
        return SpecificationOrdering = new(orderFunction);
    }
}