using Aseme.HubSupplier.EmailNotifications.Domain;
using Aseme.Shared.Domain;

namespace Aseme.HubSupplier.EmailNotifications.Application.Get
{
    public class GetEmailNotificationService : IGetEmailNotificationService
    {
        private readonly IEmailNotificationRepository _repository;

        public GetEmailNotificationService(IEmailNotificationRepository repository)
        {
            _repository = repository;
        }

        public async Task<EmailNotification> GetAsync(long id)
        {
            return await FindEmailNotificationIfExists(id);
        }

        private async Task<EmailNotification> FindEmailNotificationIfExists(long id)
        {
            EmailNotification entity = await _repository.FindAsync(new EmailNotificationWithEmailNotificationDetails(id));
            if (null == entity) { throw new NotFoundException(ErrorCode.NOT_FOUND, EmailNotification.TableName, id); }
            return entity;
        }
    }
}