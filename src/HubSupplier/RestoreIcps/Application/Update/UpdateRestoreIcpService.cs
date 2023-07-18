using Aseme.HubSupplier.RestoreIcps.Domain;
using Aseme.Shared.Domain;
using Aseme.Shared.Infrastructure.PubSub.Publisher;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data.Entity.Validation;

namespace Aseme.HubSupplier.RestoreIcps.Application.Update
{
    public class UpdateRestoreIcpService : IUpdateRestoreIcpService
    {
        private readonly IRestoreIcpRepository _repository;
        private readonly IMapper _mapper;
        private readonly IPubSubPublisher _publisher;

        public UpdateRestoreIcpService(IRestoreIcpRepository repository, IMapper mapper, IServiceProvider serviceProvider)
        {
            _repository = repository;
            _mapper = mapper;

            _publisher = serviceProvider.GetRequiredService<IPubSubPublisher>();
        }

        public async Task<RestoreIcp> UpdateAsync(long id, RestoreIcp data)
        {
            try
            {
                RestoreIcp entity = await FindRestoreIcpIfExists(id);
                _mapper.Map(data, entity);

                var result = await _repository.Update(entity);

                RestoreIcpWasUpdatedMessage restoreIcpWasUpdatedMessage = new(result);
                _publisher.Publish(restoreIcpWasUpdatedMessage);

                return result;
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

        private async Task<RestoreIcp> FindRestoreIcpIfExists(long id)
        {
            RestoreIcp entity = await _repository.FindAsync(new RestoreIcpWithRestoreIcpDetails(id));
            if (null == entity) { throw new NotFoundException(ErrorCode.NOT_FOUND, RestoreIcp.TableName, id); }
            return entity;
        }
    }
}