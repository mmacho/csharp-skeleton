using Aseme.Shared.Infrastructure.Http;

namespace Aseme.HubSupplier.EmailNotifications.Domain
{
    public class EmailNotificationFilter : PaginateFilter
    {
        public long EntityId { get; set; }
    }
}