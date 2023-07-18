using Aseme.HubSupplier.Shared.Infrastructure.Persistence.EntityFramework;
using Aseme.Shared.Infrastructure.Persistence.EntityFramework;

namespace Aseme.HubSupplier.WebhookNotifications.Domain
{
    public class WebhookNotificationRepository : BaseRepository<WebhookNotification>, IWebhookNotificationRepository
    {
        public WebhookNotificationRepository(HubSuppliersDbContext context) : base(context)
        {
        }
    }
}