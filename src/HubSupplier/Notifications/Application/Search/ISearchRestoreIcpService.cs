using Aseme.HubSupplier.Notifications.Domain;
using Aseme.HubSupplier.Shared.Domain.Notification;
using Aseme.Shared.Domain;

namespace Aseme.HubSupplier.Notifications.Application.Search
{
    public interface ISearchNotificationService
    {
        Task<PageResult<BaseNotification>> SearchAsync(NotificationFilter filter);
    }
}