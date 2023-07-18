using Aseme.HubSupplier.Shared.Domain.Notification;
using Aseme.HubSupplier.Shared.Infrastructure.Persistence.EntityFramework;
using Aseme.Shared.Infrastructure.Persistence.EntityFramework;

namespace Aseme.HubSupplier.Notifications.Domain
{
    public class NotificationRepository : BaseRepository<BaseNotification>, INotificationRepository
    {
        public NotificationRepository(HubSuppliersDbContext context) : base(context)
        {
        }
    }
}