using Aseme.HubSupplier.Shared.Domain.Notification;

namespace Aseme.HubSupplier.EmailNotifications.Domain
{
    public class EmailNotification : BaseNotification
    {
        public const string TableName = "EmailNotification";

        public string EmailAddress { get; set; }
    }
}