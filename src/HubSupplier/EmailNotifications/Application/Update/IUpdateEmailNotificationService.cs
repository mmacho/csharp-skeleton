using Aseme.HubSupplier.EmailNotifications.Domain;
using Aseme.HubSupplier.RestoreIcps.Domain;

namespace Aseme.HubSupplier.EmailNotifications.Application.Update
{
    public interface IUpdateEmailNotificationService
    {
        Task<EmailNotification> UpdateAsync(long id, RestoreIcp data);
    }
}