using Aseme.HubSupplier.HttpLogs.Domain;

namespace Aseme.HubSupplier.HttpLogs.Application.Update
{
    public interface IUpdateHttpLogService
    {
        Task<HttpLog> UpdateAsync(int id, HttpLog data);
    }
}