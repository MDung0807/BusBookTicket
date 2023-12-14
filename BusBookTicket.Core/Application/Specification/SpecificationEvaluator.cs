using BusBookTicket.Core.Application.Specification.Interfaces;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;
using Microsoft.EntityFrameworkCore;

namespace BusBookTicket.Core.Application.Specification;

public abstract class SpecificationEvaluator<T> where T : BaseEntity
{
    public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification, DbSet<T>dbSet)
    {
        var query = inputQuery;

        // Apply custom SQL query if any
        if (!string.IsNullOrEmpty(specification.SqlQuery))
        {
            query = dbSet.FromSqlRaw(specification.SqlQuery, specification.GetParameters().ToArray());
        }
        // modify the IQueryable using the specification's criteria expression
        if (specification == null)
        {
            return query;
        }
        if (specification.Criteria != null)
        {
            query = query.Where(specification.Criteria);
        }
        if (specification.CheckStatus)
        {
            query = query.Where(x => x.Status == (int)EnumsApp.Active || x.Status == (int)EnumsApp.Complete);
        }
        
        // Includes all expression-based includes
        query = specification.Includes.Aggregate(query,
            (current, include) => current.Include(include));

        // Include any string-based include statements
        query = specification.IncludeStrings.Aggregate(query,
            (current, include) => current.Include(include));

        // Include any conditions-based include statements
        query = specification.IncludeConditions.Aggregate(query, (current, condition) => condition(current));
        // Apply ordering if expressions are set
        if (specification.OrderBy != null)
        {
            query = query.OrderBy(specification.OrderBy);
        }
        else if (specification.OrderByDescending != null)
        {
            query = query.OrderByDescending(specification.OrderByDescending);
        }

        if (specification.GroupBy != null)
        {
            query = query.GroupBy(specification.GroupBy).SelectMany(x => x);
        }

        // Apply paging if enabled
        if (specification.IsPagingEnabled)
        {
            query = query.Skip(specification.Skip)
                .Take(specification.Take);
        }
        return query;
    }
}