using Aseme.HubSupplier.RestoreIcps.Domain;

namespace Aseme.HubSupplier.RestoreIcps.Application.Update
{
    public interface IUpdateRestoreIcpService
    {
        Task<RestoreIcp> UpdateAsync(long id, RestoreIcp data);
    }
}