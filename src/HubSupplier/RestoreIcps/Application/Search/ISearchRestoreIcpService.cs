using Aseme.HubSupplier.RestoreIcps.Domain;
using Aseme.Shared.Domain;

namespace Aseme.HubSupplier.RestoreIcps.Application.Search
{
    public interface ISearchRestoreIcpService
    {
        Task<PageResult<RestoreIcp>> SearchAsync(RestoreIcpFilter filter);
    }
}