using Aseme.HubSupplier.RestoreIcps.Domain;
using Aseme.HubSupplier.Shared.Domain.Operation;
using Aseme.Shared.Domain.Exceptions;
using Aseme.Shared.Infrastructure.PubSub.Publisher;
using Microsoft.Extensions.DependencyInjection;
using System.Data.Entity.Validation;
using System.Linq.Expressions;

namespace Aseme.HubSupplier.RestoreIcps.Application.Create
{
    public class CreateRestoreIcpService : ICreateRestoreIcpService
    {
        private readonly IRestoreIcpRepository _repository;

        private readonly IPubSubPublisher _publisher;

        public CreateRestoreIcpService(IRestoreIcpRepository repository, IServiceProvider serviceProvider)
        {
            _repository = repository;
            _publisher = serviceProvider.GetRequiredService<IPubSubPublisher>();
        }

        public async Task<RestoreIcp> CreateAsync(RestoreIcp data)
        {
            try
            {
                Expression<Func<RestoreIcp, bool>> criteria = o => o.SupplyPoint == data.SupplyPoint && o.OperationStatus != OperationStatusType.EXE;
                bool onlineMeterExists = await _repository.Exists(criteria);
                if (onlineMeterExists) { throw new DomainException(ErrorCode.ALREADY_EXISTS, "Already exists a restore ICP"); }
                data.OperationStatus = OperationStatusType.RECEIVED;

                var result = await _repository.Save(data);

                RestoreIcpWasCreatedMessage restoreIcpWasCreatedMessage = new(result);
                _publisher.Publish(restoreIcpWasCreatedMessage);

                return result;
            }
            catch (DbEntityValidationException ex)
            {
                throw new EntityValidationException(ex);
            }
        }
    }
}