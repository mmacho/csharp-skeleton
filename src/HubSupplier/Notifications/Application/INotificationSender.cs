using Aseme.HubSupplier.Shared.Domain.Notification;

namespace Aseme.HubSupplier.Notifications.Application
{
    public interface INotificationSender
    {
        public Type GetSenderType();
        public Task SendAsync<T>(T notification) where T : BaseNotification;
    }
}