using Aseme.Shared.Domain.Support;
using Hsd.Users.Domain;

namespace Hsd.Users.Aplication.Search
{
    public class SearchUserService : ISearchUserService
    {
        private readonly IUserRepository _repository;

        public SearchUserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<PageResult<User>> SearchAsync(UserFilter filter)
        {
            UserWithUserDetails specification = new(filter);
            if (filter.PageNumber != null && filter.PageSize != null) { specification.ApplyPaging((int)filter.PageNumber, (int)filter.PageSize); }
            return await _repository.SearchAsync(specification);
        }
    }
}