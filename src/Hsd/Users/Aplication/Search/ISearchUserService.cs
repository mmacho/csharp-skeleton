using Aseme.Shared.Domain.Support;
using Hsd.Users.Domain;

namespace Hsd.Users.Aplication.Search
{
    public interface ISearchUserService
    {
        Task<PageResult<User>> SearchAsync(UserFilter filter);
    }
}