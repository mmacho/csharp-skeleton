using Aseme.Shared.Domain;

namespace Aseme.HubSupplier.Notifications.Domain
{
    public class NotificationFilter : PaginateFilter
    {
        public int SentState { get; set; }
    }
}