using Aseme.HubSupplier.RestoreIcps.Domain;
using Aseme.Shared.Domain;

namespace Aseme.HubSupplier.RestoreIcps.Application.Search
{
    public class SearchRestoreIcpService : ISearchRestoreIcpService
    {
        private readonly IRestoreIcpRepository _repository;

        public SearchRestoreIcpService(IRestoreIcpRepository repository)
        {
            _repository = repository;
        }

        public async Task<PageResult<RestoreIcp>> SearchAsync(RestoreIcpFilter filter)
        {
            RestoreIcpWithRestoreIcpDetails specification = new(filter);
            if (filter.PageNumber != null && filter.PageSize != null) { specification.ApplyPaging((int)filter.PageNumber, (int)filter.PageSize); }
            return await _repository.Search(specification);
        }
    }
}