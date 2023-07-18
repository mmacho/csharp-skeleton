using Aseme.HubSupplier.Notifications.Domain;

namespace Aseme.HubSupplier.EmailNotifications.Application
{
    public interface IEmailSenderService
    {
        Type GetSenderType();
        Task SendAsync<T>(T notification) where T : BaseNotification;
    }
}