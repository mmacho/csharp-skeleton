using Aseme.HubSupplier.Shared.Domain.Notification;

namespace Aseme.HubSupplier.WebhookNotifications.Application
{
    public interface IWebhookSenderService
    {
        Type GetSenderType();
        Task SendAsync<T>(T notification) where T : BaseNotification;
    }
}