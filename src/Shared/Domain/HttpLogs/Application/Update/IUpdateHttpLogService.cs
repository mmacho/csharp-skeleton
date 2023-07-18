using Aseme.Shared.Domain.HttpLogs.Domain;

namespace Aseme.Shared.Domain.HttpLogs.Application.Update
{
    public interface IUpdateHttpLogService
    {
        Task<HttpLog> UpdateAsync(int id, HttpLog data);
    }
}