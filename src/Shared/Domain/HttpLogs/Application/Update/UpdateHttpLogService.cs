using Aseme.Shared.Domain.HttpLogs.Domain;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Validation;

namespace Aseme.Shared.Domain.HttpLogs.Application.Update
{
    public class UpdateHttpLogService : IUpdateHttpLogService
    {
        private readonly IHttpLogRepository _repository;
        private readonly IMapper _mapper;

        public UpdateHttpLogService(IHttpLogRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<HttpLog> UpdateAsync(int id, HttpLog data)
        {
            try
            {
                HttpLog entity = await FindHttpLogIfExists(id);
                _mapper.Map(data, entity);
                return await _repository.UpdateAsync(entity);
            }
            catch (DbEntityValidationException ex)
            {
                throw new EntityValidationException(ex);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new StaleStateIdentifiedException(ErrorCode.CONFLICT, HttpLog.TableName, id);
            }
        }

        private async Task<HttpLog> FindHttpLogIfExists(int id)
        {
            HttpLog entity = await _repository.FindAsync(new HttpLogWithHttpLogDetails(id));
            if (null == entity) { throw new NotFoundException(ErrorCode.NOT_FOUND, HttpLog.TableName, id); }
            return entity;
        }
    }
}