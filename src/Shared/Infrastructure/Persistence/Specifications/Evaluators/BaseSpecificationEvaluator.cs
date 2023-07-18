using Aseme.Shared.Domain;
using Aseme.Shared.Infrastructure.Persistence.Specifications.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Aseme.Shared.Infrastructure.Persistence.Specifications.Evaluators
{
    public class BaseSpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> specification)
        {
            var query = inputQuery;

            // modify the IQueryable using the specification's criteria expression
            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }

            // Includes all expression-based includes
            query = specification.Includes.Aggregate(query,
                                    (current, include) => current.Include(include));

            // Include any string-based include statements
            query = specification.IncludeStrings.Aggregate(query,
                                    (current, include) => current.Include(include));

            // Apply ordering if expressions are set
            if (specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }
            else if (specification.OrderByDescending != null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }
            else
            {
                // default
                query.OrderBy(a => a.Id);
            }

            // Apply groupby if expressions are set
            if (specification.GroupBy != null)
            {
                query = query.GroupBy(specification.GroupBy).SelectMany(x => x);
            }

            // Apply paging if enabled
            if (specification.IsPagingEnabled)
            {
                query = query.Skip((specification.Skip - 1) * specification.Take)
                             .Take(specification.Take);
            }
            return query;
        }
    }
}