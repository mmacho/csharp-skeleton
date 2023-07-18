using Aseme.HubSupplier.Notifications.Application;
using Aseme.HubSupplier.Notifications.Domain;
using Aseme.HubSupplier.WebhookNotifications.Domain;
using Microsoft.Extensions.Logging;

namespace Aseme.HubSupplier.WebhookNotifications.Application
{
    public class WebhookSenderService : INotificationSender, IWebhookSenderService
    {
        private readonly ILogger<WebhookSenderService> _logger;

        public WebhookSenderService(ILogger<WebhookSenderService> logger)
        {
            _logger = logger;
        }

        public Type GetSenderType()
        {
            return typeof(WebhookNotification);
        }

        public async Task SendAsync<T>(T notification) where T : BaseNotification
        {
            if (notification is not WebhookNotification webhookNotification)
            {
                throw new ArgumentException("Invalid notification type");
            }

            _logger.LogInformation("Sending request to " + webhookNotification.Url);
            await Task.Delay(1_000);
        }
    }
}