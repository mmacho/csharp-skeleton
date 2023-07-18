using Aseme.HubSupplier.EmailNotifications.Domain;
using Aseme.Shared.Domain.Support;

namespace Aseme.HubSupplier.EmailNotifications.Application.Search
{
    public interface ISearchEmailNotificationService
    {
        Task<PageResult<EmailNotification>> SearchAsync(EmailNotificationFilter filter);
    }
}