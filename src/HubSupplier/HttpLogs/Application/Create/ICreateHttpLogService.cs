using Aseme.HubSupplier.HttpLogs.Domain;

namespace Aseme.HubSupplier.HttpLogs.Application.Create
{
    public interface ICreateHttpLogService
    {
        Task<HttpLog> CreateAsync(HttpLog data);
    }
}