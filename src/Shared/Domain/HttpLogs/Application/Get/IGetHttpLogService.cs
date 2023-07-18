using Aseme.Shared.Domain.HttpLogs.Domain;

namespace Aseme.Shared.Domain.HttpLogs.Application.Get
{
    public interface IGetHttpLogService
    {
        Task<HttpLog> GetAsync(int id);
    }
}