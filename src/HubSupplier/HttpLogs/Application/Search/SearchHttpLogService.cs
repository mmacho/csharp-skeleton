using Aseme.HubSupplier.HttpLogs.Domain;
using Aseme.Shared.Domain.Support;

namespace Aseme.HubSupplier.HttpLogs.Application.Search
{
    public class SearchHttpLogService : ISearchHttpLogService
    {
        private readonly IHttpLogRepository _repository;

        public SearchHttpLogService(IHttpLogRepository repository)
        {
            _repository = repository;
        }

        public async Task<PageResult<HttpLog>> SearchAsync(HttpLogFilter filter)
        {
            HttpLogWithHttpLogDetails specification = new(filter);
            if (filter.PageNumber != null && filter.PageSize != null) { specification.ApplyPaging((int)filter.PageNumber, (int)filter.PageSize); }
            return await _repository.SearchAsync(specification);
        }
    }
}