using Aseme.HubSupplier.RestoreIcps.Domain;

namespace Aseme.HubSupplier.RestoreIcps.Application.Create
{
    public interface ICreateRestoreIcpService
    {
        Task<RestoreIcp> CreateAsync(RestoreIcp data);
    }
}