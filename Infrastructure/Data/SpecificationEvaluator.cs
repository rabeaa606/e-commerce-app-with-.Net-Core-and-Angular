
namespace Infrastructure.Data;
public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
{
    public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,
    ISpecification<TEntity> spec)
    {
        var query = inputQuery;

        if (spec.Criteria != null) query = query.Where(spec.Criteria);// filter :   Example: p=>p.ProductTpeId ==Id

        if (spec.OrderBy != null) query = query.OrderBy(spec.OrderBy);// sort : Example: p=>p.price 

        if (spec.OrderByDescending != null) query = query.OrderByDescending(spec.OrderByDescending);//sort :  Example: p=>p.price 

        if (spec.IsPagingEnabled) query = query.Skip(spec.Skip).Take(spec.Take);//pagination :  Example: skip 5  take 10

        query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));//add : includes

        return query;
    }
}
