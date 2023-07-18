using Aseme.Shared.Domain;
using Hsc.Logins.Domain;

namespace Hsc.Logins.Aplication.Search
{
    public class SearchLoginService : ISearchLoginService
    {
        private readonly ILoginRepository _repository;

        public SearchLoginService(ILoginRepository repository)
        {
            _repository = repository;
        }

        public async Task<PageResult<Login>> SearchAsync(LoginFilter filter)
        {
            LoginWithLoginDetails specification = new(filter);
            if (filter.PageNumber != null && filter.PageSize != null) { specification.ApplyPaging((int)filter.PageNumber, (int)filter.PageSize); }
            return await _repository.SearchAsync(specification);
        }
    }
}