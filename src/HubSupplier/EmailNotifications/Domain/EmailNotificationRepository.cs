using Aseme.HubSupplier.Shared.Infrastructure.Persistence.EntityFramework;
using Aseme.Shared.Infrastructure.Persistence.EntityFramework;

namespace Aseme.HubSupplier.EmailNotifications.Domain
{
    public class EmailNotificationRepository : BaseRepository<EmailNotification>, IEmailNotificationRepository
    {
        public EmailNotificationRepository(HubSuppliersDbContext context) : base(context)
        {
        }
    }
}