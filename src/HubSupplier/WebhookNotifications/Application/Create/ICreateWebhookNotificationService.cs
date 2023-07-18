using Aseme.HubSupplier.WebhookNotifications.Domain;

namespace Aseme.HubSupplier.WebhookNotifications.Application.Create
{
    public interface ICreateWebhookNotificationService
    {
        Task<WebhookNotification> CreateAsync(WebhookNotification data);
    }
}