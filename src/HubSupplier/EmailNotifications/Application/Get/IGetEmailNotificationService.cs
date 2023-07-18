using Aseme.HubSupplier.EmailNotifications.Domain;

namespace Aseme.HubSupplier.EmailNotifications.Application.Get
{
    public interface IGetEmailNotificationService
    {
        Task<EmailNotification> GetAsync(long id);
    }
}