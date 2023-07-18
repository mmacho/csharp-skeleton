using Aseme.Shared.Domain;

namespace Aseme.HubSupplier.EmailNotifications.Domain
{
    public class EmailNotificationFilter : PaginateFilter
    {
        public long EntityId { get; set; }
    }
}