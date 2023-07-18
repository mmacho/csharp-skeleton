using Aseme.HubSupplier.RestoreIcps.Domain;
using Aseme.Shared.Domain;

namespace Aseme.HubSupplier.RestoreIcps.Application.Get
{
    public class GetRestoreIcpService : IGetRestoreIcpService
    {
        private readonly IRestoreIcpRepository _repository;

        public GetRestoreIcpService(IRestoreIcpRepository repository)
        {
            _repository = repository;
        }

        public async Task<RestoreIcp> GetAsync(long id)
        {
            return await FindRestoreIcpIfExists(id);
        }

        private async Task<RestoreIcp> FindRestoreIcpIfExists(long id)
        {
            RestoreIcp entity = await _repository.Find(new RestoreIcpWithRestoreIcpDetails(id));
            if (null == entity) { throw new NotFoundException(ErrorCode.NOT_FOUND, RestoreIcp.TableName, id); }
            return entity;
        }
    }
}