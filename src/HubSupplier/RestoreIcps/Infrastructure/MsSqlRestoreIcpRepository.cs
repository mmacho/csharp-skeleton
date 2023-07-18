using Aseme.HubSupplier.RestoreIcps.Domain;
using Aseme.HubSupplier.Shared.Infrastructure.Persistence.EntityFramework;
using Aseme.Shared.Domain.Support;
using Aseme.Shared.Infrastructure.Persistence.EntityFramework;
using Aseme.Shared.Infrastructure.Persistence.Specifications.Model;
using System.Linq.Expressions;

namespace Aseme.HubSupplier.RestoreIcps.Infrastructure
{
    public class MsSqlRestoreIcpRepository : BaseRepository<RestoreIcp>, IRestoreIcpRepository
    {
        public MsSqlRestoreIcpRepository(HubSuppliersDbContext context) : base(context)
        {
        }

        public async Task<RestoreIcp> Save(RestoreIcp restoreIcp)
        {
            return await base.AddAsync(restoreIcp);
        }

        public new async Task<bool> Exists(Expression<Func<RestoreIcp, bool>> predicate)
        {
            return await base.ExistsAsync(predicate);
        }

        public new async Task<RestoreIcp> Find(ISpecification<RestoreIcp> specification = null)
        {
            return await base.FindAsync(specification);
        }

        public new async Task<RestoreIcp> FindById(long id)
        {
            return await base.FindByIdAsync(id);
        }

        public async Task Delete(RestoreIcp restoreIcp)
        {
            await base.RemoveAsync(restoreIcp);
        }

        public new async Task<RestoreIcp> Update(RestoreIcp restoreIcp)
        {
            return await base.UpdateAsync(restoreIcp);
        }

        public new async Task<PageResult<RestoreIcp>> Search(ISpecification<RestoreIcp> specification)
        {
            return await base.SearchAsync(specification);
        }
    }
}
