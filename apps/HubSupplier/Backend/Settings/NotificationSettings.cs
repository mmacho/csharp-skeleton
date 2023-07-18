using Aseme.HubSupplier.Notifications.Infrastructure.BackgroundServices.Notifications;

namespace Aseme.Apps.HubSupplier.Backend.Settings
{
    public class NotificationSettings : INotificationSettings
    {
        public int IntervalMinutes { get; set; }
    }
}