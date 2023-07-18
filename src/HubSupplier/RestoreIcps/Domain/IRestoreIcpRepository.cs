using Aseme.Shared.Domain;
using Aseme.Shared.Infrastructure.Persistence.Specifications.Model;
using System.Linq.Expressions;

namespace Aseme.HubSupplier.RestoreIcps.Domain
{
    public interface IRestoreIcpRepository
    {
        Task<RestoreIcp> Save(RestoreIcp restoreIcp);

        Task<RestoreIcp> FindById(long id);

        Task<RestoreIcp> Find(ISpecification<RestoreIcp> specification = null);

        Task<bool> Exists(Expression<Func<RestoreIcp, bool>> predicate);

        Task<PageResult<RestoreIcp>> Search(ISpecification<RestoreIcp> specification = null);

        Task Delete(RestoreIcp restoreIcp);

        Task<RestoreIcp> Update(RestoreIcp restoreIcp);
    }
}
