using Aseme.Shared.Infrastructure.Persistence.Specifications.Model;

namespace Aseme.HubSupplier.Notifications.Domain
{
    public class NotificationWithNotificationDetails : Specification<BaseNotification>
    {
        public NotificationWithNotificationDetails()
        {
        }

        public NotificationWithNotificationDetails(int id) : base(x => x.Id.Equals(id))
        {
        }

        public NotificationWithNotificationDetails(NotificationFilter filter) : base(x => x.SentState.Equals(filter.SentState))
        {
        }
    }
}