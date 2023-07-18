using Aseme.HubSupplier.Notifications.Domain;
using Aseme.Shared.Domain.Support;

namespace Aseme.HubSupplier.Notifications.Application.Search
{
    public interface ISearchNotificationService
    {
        Task<PageResult<BaseNotification>> SearchAsync(NotificationFilter filter);
    }
}