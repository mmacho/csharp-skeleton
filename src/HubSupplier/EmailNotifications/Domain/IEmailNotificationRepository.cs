using Aseme.Shared.Infrastructure.Persistence.EntityFramework;

namespace Aseme.HubSupplier.EmailNotifications.Domain
{
    public interface IEmailNotificationRepository : IBaseRepository<EmailNotification>
    {
    }
}