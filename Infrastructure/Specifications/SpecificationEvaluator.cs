using Domain.Primitives;
using System.Linq.Expressions;

namespace Infrastructure.Specifications;

internal static class SpecificationEvaluator
{
    public static IQueryable<TEntity> WithSpecification<TEntity>(
        this IQueryable<TEntity> query,
        Specification<TEntity> specification) where TEntity : Entity
    {
        ApplyCretaria(ref query, specification.Cretaria);
        ApplyConditions(ref query, specification.Conditions);
        ApplyOrdering(ref query, specification.SpecificationOrdering);

        return query;
    }

    static void ApplyOrdering<TEntity>(
        ref IQueryable<TEntity> query,
        SpecificationOrdering<TEntity>? specificationOrdering) where TEntity : Entity
    {
        var ordering = specificationOrdering?.Ordering;
        if (ordering is null || !ordering.Any())
        {
            return;
        }

        IOrderedQueryable<TEntity> orderedQuery = specificationOrdering!.IsAsc
                                                        ? query.OrderBy(ordering[0])
                                                        : query.OrderByDescending(ordering[0]);
        query = ordering.Skip(1).Aggregate(
            orderedQuery,
            (currentOrderedQuery, orderFunction) => currentOrderedQuery.ThenBy(orderFunction)
            );

    }

    static void ApplyCretaria<TEntity>(
        ref IQueryable<TEntity> query,
        Expression<Func<TEntity, bool>>? cretaria) where TEntity : Entity
    {
        if (cretaria is { })
        {
            query = query.Where(cretaria);
        }
    }

    static void ApplyConditions<TEntity>(
        ref IQueryable<TEntity> query,
        IReadOnlyList<Expression<Func<TEntity, bool>>> conditions) where TEntity : Entity
    {
        query = conditions.Aggregate(
             query,
            (currentQuery, condition) => currentQuery.Where(condition)
            );
    }
}