using Aseme.HubSupplier.EmailNotifications.Domain;
using Aseme.Shared.Domain;
using System.Data.Entity.Validation;

namespace Aseme.HubSupplier.EmailNotifications.Application.Create
{
    public class CreateEmailNotificationService : ICreateEmailNotificationService
    {
        private readonly IEmailNotificationRepository _repository;

        public CreateEmailNotificationService(IEmailNotificationRepository repository)
        {
            _repository = repository;
        }

        public async Task<EmailNotification> CreateAsync(EmailNotification data)
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