using Aseme.Shared.Domain.HttpLogs.Domain;

namespace Aseme.Shared.Domain.HttpLogs.Application.Search
{
    public interface ISearchHttpLogService
    {
        Task<PageResult<HttpLog>> SearchAsync(HttpLogFilter filter);
    }
}