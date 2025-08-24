using Domain.Entities;
using Domain.Interface;

namespace Infrastructure.Repository;

public class SpecificationEvaluator<T> where T:BaseEntity
{
    public static IQueryable<T> GetQuery(IQueryable<T> query,ISpecification<T> spec)
    {
        if(spec.criteria != null)
        {
            query = query.Where(spec.criteria);
        }
        if (spec.orderBy!=null)
        {
            query = query.OrderBy(spec.orderBy);
        }
        if (spec.orderByDescending != null)
        {
            query = query.OrderByDescending(spec.orderByDescending);
        }
        if (spec.IsDistinct)
        {
            query.Distinct();
        }
        return query;
    }
    public static IQueryable<TResult> GetQuery<TSpec,TResult>(IQueryable<T> query, ISpecification<T,TResult> spec)
    {
        if (spec.criteria != null)
        {
            query = query.Where(spec.criteria);
        }
        if (spec.orderBy != null)
        {
            query = query.OrderBy(spec.orderBy);
        }
        if (spec.orderByDescending != null)
        {
            query = query.OrderByDescending(spec.orderByDescending);
        }
        
        var selectQuery = query as IQueryable<TResult>;
        if (spec.Select != null)
        {
            selectQuery = query.Select(spec.Select);
        }
        if (spec.IsDistinct)
        {
            selectQuery= selectQuery?.Distinct();
        }
        return selectQuery ?? query.Cast<TResult>();
    }
}