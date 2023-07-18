using Aseme.Shared.Infrastructure.Persistence.EntityFramework;

namespace Aseme.HubSupplier.Notifications.Domain
{
    public interface INotificationRepository : IBaseRepository<BaseNotification>
    {
    }
}