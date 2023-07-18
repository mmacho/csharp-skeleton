using Aseme.Shared.Domain.Support;
using Aseme.Shared.Infrastructure.Persistence.Specifications.Evaluators;
using Aseme.Shared.Infrastructure.Persistence.Specifications.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Aseme.Shared.Infrastructure.Persistence.EntityFramework
{
    public class LegacyRepository<TEntity> : ILegacyRepository<TEntity> where TEntity : LegacyEntity
    {
        protected readonly DbContext _context;

        public LegacyRepository(DbContext context)
        {
            _context = context;
        }

        public TEntity Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
            _context.SaveChanges();
        }

        public bool Exists(ISpecification<TEntity> specification = null)
        {
            return Count(specification) > 0;
        }

        public async Task<bool> ExistsAsync(ISpecification<TEntity> specification = null)
        {
            return await CountAsync(specification) > 0;
        }

        public bool Exists(Expression<Func<TEntity, bool>> predicate)
        {
            return Count(predicate) > 0;
        }

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await CountAsync(predicate) > 0;
        }

        public int Count(ISpecification<TEntity> specification = null)
        {
            return ApplySpecification(specification).Count();
        }

        public async Task<int> CountAsync(ISpecification<TEntity> specification = null)
        {
            return await ApplySpecification(specification).CountAsync();
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate).Count();
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().Where(predicate).CountAsync();
        }

        public TEntity FindById(long id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public async Task<TEntity> FindByIdAsync(long id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public TEntity Find(ISpecification<TEntity> specification = null)
        {
            IQueryable<TEntity> query = LegacySpecificationEvaluator<TEntity>.GetQuery(_context.Set<TEntity>().AsQueryable(), specification); ;
            return query.FirstOrDefault();
        }

        public async Task<TEntity> FindAsync(ISpecification<TEntity> specification = null)
        {
            IQueryable<TEntity> query = LegacySpecificationEvaluator<TEntity>.GetQuery(_context.Set<TEntity>().AsQueryable(), specification); ;
            return await query.FirstOrDefaultAsync();
        }

        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
        }

        public async Task RemoveAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
            _context.SaveChanges();
        }

        public PageResult<TEntity> Search(ISpecification<TEntity> specification = null)
        {
            IQueryable<TEntity> query = ApplySpecification(specification);
            List<TEntity> pagedData = query.ToList();
            int count = query.Count();
            return PageResult<TEntity>.Page(pagedData, count, specification.Skip, specification.Take);
        }

        public async Task<PageResult<TEntity>> SearchAsync(ISpecification<TEntity> specification = null)
        {
            IQueryable<TEntity> query = ApplySpecification(specification);
            List<TEntity> pagedData = await query.ToListAsync();

            bool isPagingEnabled = specification.IsPagingEnabled;

            // TODO: revisar con Manu alternativas
            // La query que se utiliza para el Count() contiene el filtro de Skip/Take
            // por lo que hay que quitarlo para obtener el número de registros real sin paginado.
            // Esto es un workaround de momento para que pueda obtener el número de páginas correcto

            // Quizás lo mejor seria mover el uso de Skip/Take fuera de SpecificationEvaluator.cs para
            // poder activar su uso dependiendo de si se está generando la respuesta u obteniendo el recuento total
            int count = RemoveSkipTakeFromQuery(query).Count();

            // Set page number to 1 if pagination is disabled
            int skip = isPagingEnabled ? specification.Skip : 1;

            // Set total count to rows count if pagination is disabled
            int take = isPagingEnabled ? specification.Take : count;

            return PageResult<TEntity>.Page(pagedData, count, skip, take);
        }

        // TODO: remove this method
        private static IQueryable<TEntity> RemoveSkipTakeFromQuery(IQueryable<TEntity> query)
        {
            MethodCallExpression methodCallExpression = query.Expression as MethodCallExpression;

            if (methodCallExpression != null && (methodCallExpression.Method.Name == "Skip" || methodCallExpression.Method.Name == "Take"))
            {
                return query.Provider.CreateQuery<TEntity>(methodCallExpression.Arguments[0]);
            }

            return query;
        }

        public TEntity Update(TEntity entity)
        {
            _context.Set<TEntity>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().UpdateRange(entities);
            _context.SaveChanges();
        }

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec)
        {
            return LegacySpecificationEvaluator<TEntity>.GetQuery(_context.Set<TEntity>().AsQueryable(), spec);
        }
    }
}