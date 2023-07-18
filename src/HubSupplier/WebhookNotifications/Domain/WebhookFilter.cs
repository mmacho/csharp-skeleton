using Aseme.Shared.Infrastructure.Http;

namespace Aseme.HubSupplier.WebhookNotifications.Domain
{
    public class WebhookFilter : PaginateFilter
    {
        public long EntityId { get; set; }
    }
}