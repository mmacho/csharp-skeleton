using Aseme.HubSupplier.EmailNotifications.Domain;
using Aseme.HubSupplier.RestoreIcps.Domain;
using Aseme.Shared.Domain;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Validation;

namespace Aseme.HubSupplier.EmailNotifications.Application.Update
{
    public class UpdateEmailNotificationService : IUpdateEmailNotificationService
    {
        private readonly IEmailNotificationRepository _repository;

        private readonly IMapper _mapper;

        public UpdateEmailNotificationService(IEmailNotificationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<EmailNotification> UpdateAsync(long id, RestoreIcp data)
        {
            try
            {
                EmailNotification entity = await FindEmailNotificationIfExists(id);
                _mapper.Map(data, entity);
                return await _repository.UpdateAsync(entity);
            }
            catch (DbEntityValidationException ex)
            {
                throw new EntityValidationException(ex);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new StaleStateIdentifiedException(ErrorCode.CONFLICT, RestoreIcp.TableName, id);
            }
        }

        private async Task<EmailNotification> FindEmailNotificationIfExists(long id)
        {
            EmailNotification entity = await _repository.FindAsync(new EmailNotificationWithEmailNotificationDetails(id));
            if (null == entity) { throw new NotFoundException(ErrorCode.NOT_FOUND, EmailNotification.TableName, id); }
            return entity;
        }
    }
}