using Aseme.HubSupplier.EmailNotifications.Domain;
using Aseme.Shared.Infrastructure.Persistence.Specifications.Model;

namespace Aseme.HubSupplier.WebhookNotifications.Domain
{
    public class WebhookNotificationWithWebhookNotificationDetails : Specification<WebhookNotification>
    {
        public WebhookNotificationWithWebhookNotificationDetails()
        {
        }

        public WebhookNotificationWithWebhookNotificationDetails(long id) : base(x => x.Id.Equals(id))
        {
        }

        public WebhookNotificationWithWebhookNotificationDetails(EmailNotificationFilter filter) : base(x => x.EntityId.Equals(filter.EntityId))
        {
        }
    }
}