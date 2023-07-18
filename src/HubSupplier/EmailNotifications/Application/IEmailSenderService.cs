using Aseme.HubSupplier.Shared.Domain.Notification;

namespace Aseme.HubSupplier.EmailNotifications.Application
{
    public interface IEmailSenderService
    {
        Type GetSenderType();
        Task SendAsync<T>(T notification) where T : BaseNotification;
    }
}