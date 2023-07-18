using Aseme.HubSupplier.EmailNotifications.Domain;

namespace Aseme.HubSupplier.EmailNotifications.Application.Delete
{
    public class DeleteEmailNotificationService : IDeleteEmailNotificationService
    {
        private readonly IEmailNotificationRepository _repository;

        public DeleteEmailNotificationService(IEmailNotificationRepository repository)
        {
            _repository = repository;
        }

        public async Task DeleteAsync(long id)
        {
            EmailNotification entity = await _repository.FindByIdAsync(id);
            await _repository.RemoveAsync(entity);
        }
    }
}