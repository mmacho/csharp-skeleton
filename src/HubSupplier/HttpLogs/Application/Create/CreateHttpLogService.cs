using Aseme.HubSupplier.HttpLogs.Domain;
using Aseme.Shared.Domain.Exceptions;
using System.Data.Entity.Validation;

namespace Aseme.HubSupplier.HttpLogs.Application.Create
{
    public class CreateHttpLogService : ICreateHttpLogService
    {
        private readonly IHttpLogRepository _repository;

        public CreateHttpLogService(IHttpLogRepository repository)
        {
            _repository = repository;
        }

        public async Task<HttpLog> CreateAsync(HttpLog data)
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