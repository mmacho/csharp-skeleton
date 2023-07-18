using Aseme.HubSupplier.RestoreIcps.Domain;

namespace Aseme.HubSupplier.RestoreIcps.Application.Delete
{
    public class DeleteRestoreIcpService : IDeleteRestoreIcpService
    {
        private readonly IRestoreIcpRepository _repository;

        public DeleteRestoreIcpService(IRestoreIcpRepository repository)
        {
            _repository = repository;
        }

        public async Task DeleteAsync(long id)
        {
            RestoreIcp entity = await _repository.FindById(id);
            await _repository.Delete(entity);
        }
    }
}