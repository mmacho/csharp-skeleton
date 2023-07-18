using Aseme.Shared.Domain.HttpLogs.Domain;

namespace Aseme.Shared.Domain.HttpLogs.Application.Delete
{
    public class DeleteHttpLogService : IDeleteHttpLogService
    {
        private readonly IHttpLogRepository _repository;

        public DeleteHttpLogService(IHttpLogRepository repository)
        {
            _repository = repository;
        }

        public async Task DeleteAsync(int id)
        {
            HttpLog entity = await _repository.FindByIdAsync(id);
            await _repository.RemoveAsync(entity);
        }
    }
}