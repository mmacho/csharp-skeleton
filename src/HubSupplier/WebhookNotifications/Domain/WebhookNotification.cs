using Aseme.HubSupplier.Notifications.Domain;

namespace Aseme.HubSupplier.WebhookNotifications.Domain
{
    public class WebhookNotification : BaseNotification
    {
        public const string TableName = "WebhookNotification";

        public string Url { get; set; }
    }
}