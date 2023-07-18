using Aseme.HubSupplier.RestoreIcps.Domain;

namespace Aseme.HubSupplier.RestoreIcps.Application.Get
{
    public interface IGetRestoreIcpService
    {
        Task<RestoreIcp> GetAsync(long id);
    }
}