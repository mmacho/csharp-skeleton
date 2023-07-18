using Aseme.HubSupplier.EmailNotifications.Domain;

namespace Aseme.HubSupplier.EmailNotifications.Application.Create
{
    public interface ICreateEmailNotificationService
    {
        Task<EmailNotification> CreateAsync(EmailNotification data);
    }
}