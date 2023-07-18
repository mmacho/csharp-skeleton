using Aseme.Shared.Domain.Support;
using Aseme.Shared.Infrastructure.Persistence.Specifications.Model;
using System.Linq.Expressions;

namespace Aseme.Shared.Infrastructure.Persistence.EntityFramework
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        TEntity Add(TEntity entity);

        Task<TEntity> AddAsync(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        int Count(ISpecification<TEntity> specification = null);

        int Count(Expression<Func<TEntity, bool>> predicate);

        Task<int> CountAsync(ISpecification<TEntity> specification = null);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);

        bool Exists(ISpecification<TEntity> specification = null);

        bool Exists(Expression<Func<TEntity, bool>> predicate);

        Task<bool> ExistsAsync(ISpecification<TEntity> specification = null);

        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);

        TEntity Find(ISpecification<TEntity> specification = null);

        Task<TEntity> FindAsync(ISpecification<TEntity> specification = null);

        TEntity FindById(long id);

        Task<TEntity> FindByIdAsync(long id);

        void Remove(TEntity entity);

        Task RemoveAsync(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);

        PageResult<TEntity> Search(ISpecification<TEntity> specification = null);

        Task<PageResult<TEntity>> SearchAsync(ISpecification<TEntity> specification = null);

        TEntity Update(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        void UpdateRange(IEnumerable<TEntity> entities);
    }
}