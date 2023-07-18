using Aseme.Shared.Infrastructure.Persistence.EntityFramework;

namespace Aseme.HubSupplier.WebhookNotifications.Domain
{
    public interface IWebhookNotificationRepository : IBaseRepository<WebhookNotification>
    {
    }
}