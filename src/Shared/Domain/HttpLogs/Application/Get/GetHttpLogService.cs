using Aseme.Shared.Domain.HttpLogs.Domain;

namespace Aseme.Shared.Domain.HttpLogs.Application.Get
{
    public class GetHttpLogService : IGetHttpLogService
    {
        private readonly IHttpLogRepository _repository;

        public GetHttpLogService(IHttpLogRepository repository)
        {
            _repository = repository;
        }

        public async Task<HttpLog> GetAsync(int id)
        {
            return await FindHttpLogIfExists(id);
        }

        private async Task<HttpLog> FindHttpLogIfExists(int id)
        {
            HttpLog entity = await _repository.FindAsync(new HttpLogWithHttpLogDetails(id));
            if (null == entity) { throw new NotFoundException(ErrorCode.NOT_FOUND, HttpLog.TableName, id); }
            return entity;
        }
    }
}