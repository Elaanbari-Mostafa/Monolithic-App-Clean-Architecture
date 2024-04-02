using Domain.Primitives;
using System.Linq.Expressions;

namespace Infrastructure.Specifications;

internal abstract class Specification<TEntity> where TEntity : Entity
{
    public Expression<Func<TEntity, bool>>? Cretaria { get; private init; }
    private bool _orderByCalled = false;
    private readonly List<Expression<Func<TEntity, bool>>> _conditions = new();
    public IReadOnlyList<Expression<Func<TEntity, bool>>> Conditions => _conditions;
    public SpecificationOrdering<TEntity>? SpecificationOrdering { get; private set; }

    protected Specification(Expression<Func<TEntity, bool>>? cretaria) => Cretaria = cretaria;

    protected void AddInclude(Expression<Func<TEntity, bool>> condition) => _conditions.Add(condition);

    protected SpecificationOrdering<TEntity> OrderBy(Expression<Func<TEntity, object>> orderFunction)
    {
        VerifyOrderFunctionTimeCalled();
        return SpecificationOrdering = new(orderFunction);
    }

    protected SpecificationOrdering<TEntity> OrderByDesc(Expression<Func<TEntity, object>> orderFunction)
    {
        VerifyOrderFunctionTimeCalled();
        return SpecificationOrdering = new(orderFunction, false);
    }

    private object VerifyOrderFunctionTimeCalled()
       => _orderByCalled
            ? throw new InvalidOperationException("Ordering function can only be called once.")
            : _orderByCalled = true;
}