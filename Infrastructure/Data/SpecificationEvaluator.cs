using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

/// <summary>
/// This class facilitates building dynamic queries by applying criteria such as
/// filtering, sorting, pagination, and projection.
/// Used with the Specification Pattern, it helps separate concerns 
/// and prevents repositories from being cluttered with query conditions.
/// </summary>
/// <typeparam name="T">The entity type being queried.</typeparam>
public class SpecificationEvaluator<T> where T : BaseEntity
{
    
    /// <summary>
    /// Applies a specification to filter, sort, and paginate entities of type <typeparamref name="T"/>.
    /// </summary>
    /// <param name="query">The base query to apply the specification to.</param>
    /// <param name="spec">The specification containing filtering, sorting, and pagination criteria.</param>
    /// <returns>An <see cref="IQueryable{T}"/> with the applied criteria.</returns>
    public static IQueryable<T> GetQuery(IQueryable<T> query, ISpecification<T> spec)
    {
        if (spec.Criteria != null)
        {
            query = query.Where(spec.Criteria); // x => x.Brand == brand
        }

        if (spec.OrderBy != null)
        {
            query = query.OrderBy(spec.OrderBy);
        }

        if (spec.OrderByDescending != null)
        {
            query = query.OrderByDescending(spec.OrderByDescending);
        }

        if (spec.IsDistinct) 
        {
            query = query.Distinct();
        }

        if (spec.IsPagingEnabled)
        {
            query = query
                .Skip(spec.Skip)
                .Take(spec.Take);
        }
        
        query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
        query = spec.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));

        return query;
    }

    /// <summary>
    /// Similar to the first method but supports projection by allowing a transformation 
    /// from type <typeparamref name="T"/> to <typeparamref name="TResult"/>.
    /// </summary>
    /// <typeparam name="TSpec">The entity type being queried.</typeparam>
    /// <typeparam name="TResult">The resulting type after applying the projection.</typeparam>
    /// <param name="query">The base query to apply the specification to.</param>
    /// <param name="spec">The specification containing filtering, sorting, pagination, and projection criteria.</param>
    /// <returns>An <see cref="IQueryable{TResult}"/> with the applied criteria.</returns>
    public static IQueryable<TResult> GetQuery<TSpec, TResult>(IQueryable<T> query, 
        ISpecification<T, TResult> spec)
    {
        if (spec.Criteria != null)
        {
            query = query.Where(spec.Criteria); // x => x.Brand == brand
        }

        if (spec.OrderBy != null)
        {
            query = query.OrderBy(spec.OrderBy);
        }

        if (spec.OrderByDescending != null)
        {
            query = query.OrderByDescending(spec.OrderByDescending);
        }

        var selectQuery = query as IQueryable<TResult>;

        if (spec.Select != null)
        {
            selectQuery = query.Select(spec.Select);
        }

        if (spec.IsDistinct)
        {
            selectQuery = selectQuery?.Distinct();
        }
        
        if (spec.IsPagingEnabled)
        {
            selectQuery = selectQuery?
                .Skip(spec.Skip)
                .Take(spec.Take);
        }

        return selectQuery ?? query.Cast<TResult>();
    }
}
