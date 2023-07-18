using Aseme.HubSupplier.EmailNotifications.Domain;
using Aseme.Shared.Domain;

namespace Aseme.HubSupplier.EmailNotifications.Application.Search
{
    public interface ISearchEmailNotificationService
    {
        Task<PageResult<EmailNotification>> SearchAsync(EmailNotificationFilter filter);
    }
}