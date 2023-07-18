using Aseme.Shared.Domain;

namespace Aseme.HubSupplier.WebhookNotifications.Domain
{
    public class WebhookFilter : PaginateFilter
    {
        public long EntityId { get; set; }
    }
}