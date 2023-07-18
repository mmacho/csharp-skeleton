using Aseme.HubSupplier.HttpLogs.Domain;
using Aseme.Shared.Domain.Support;

namespace Aseme.HubSupplier.HttpLogs.Application.Search
{
    public interface ISearchHttpLogService
    {
        Task<PageResult<HttpLog>> SearchAsync(HttpLogFilter filter);
    }
}