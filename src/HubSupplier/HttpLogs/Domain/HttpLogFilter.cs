using Aseme.Shared.Infrastructure.Http;

namespace Aseme.HubSupplier.HttpLogs.Domain
{
    public class HttpLogFilter : PaginateFilter
    {
        public long EntityId { get; set; }
    }
}