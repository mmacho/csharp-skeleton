using Aseme.HubSupplier.HttpLogs.Domain;

namespace Aseme.HubSupplier.HttpLogs.Application.Get
{
    public interface IGetHttpLogService
    {
        Task<HttpLog> GetAsync(int id);
    }
}