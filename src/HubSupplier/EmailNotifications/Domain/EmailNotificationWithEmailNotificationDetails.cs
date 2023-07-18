using Aseme.Shared.Infrastructure.Persistence.Specifications.Model;

namespace Aseme.HubSupplier.EmailNotifications.Domain
{
    public class EmailNotificationWithEmailNotificationDetails : Specification<EmailNotification>
    {
        public EmailNotificationWithEmailNotificationDetails()
        {
        }

        public EmailNotificationWithEmailNotificationDetails(long id) : base(x => x.Id.Equals(id))
        {
        }

        public EmailNotificationWithEmailNotificationDetails(EmailNotificationFilter filter) : base(x => x.EntityId.Equals(filter.EntityId))
        {
        }
    }
}