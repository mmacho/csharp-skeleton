using Aseme.HubSupplier.EmailNotifications.Domain;
using Aseme.HubSupplier.Notifications.Application;
using Aseme.HubSupplier.Notifications.Domain;
using Microsoft.Extensions.Logging;

namespace Aseme.HubSupplier.EmailNotifications.Application
{
    public class EmailSenderService : INotificationSender, IEmailSenderService
    {
        private readonly ILogger<EmailSenderService> _logger;

        public EmailSenderService(ILogger<EmailSenderService> logger)
        {
            _logger = logger;
        }

        public Type GetSenderType()
        {
            return typeof(EmailNotification);
        }

        public async Task SendAsync<T>(T notification) where T : BaseNotification
        {
            if (notification is not EmailNotification emailNotification)
            {
                throw new ArgumentException("Invalid notification type");
            }

            _logger.LogInformation("Sending email to " + emailNotification.EmailAddress);
            await Task.Delay(1_000);
        }
    }
}