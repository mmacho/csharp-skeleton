using Aseme.Shared.Domain.HttpLogs.Domain;

namespace Aseme.Shared.Domain.HttpLogs.Application.Create
{
    public interface ICreateHttpLogService
    {
        Task<HttpLog> CreateAsync(HttpLog data);
    }
}