using Aseme.HubSupplier.WebhookNotifications.Domain;
using Aseme.Shared.Domain;
using System.Data.Entity.Validation;

namespace Aseme.HubSupplier.WebhookNotifications.Application.Create
{
    public class CreateWebhookNotificationService : ICreateWebhookNotificationService
    {
        private readonly IWebhookNotificationRepository _repository;

        public CreateWebhookNotificationService(IWebhookNotificationRepository repository)
        {
            _repository = repository;
        }

        public async Task<WebhookNotification> CreateAsync(WebhookNotification data)
        {
            try
            {
                return await _repository.AddAsync(data);
            }
            catch (DbEntityValidationException ex)
            {
                throw new EntityValidationException(ex);
            }
        }
    }
}