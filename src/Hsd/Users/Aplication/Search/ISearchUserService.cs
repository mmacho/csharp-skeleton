using Aseme.Shared.Domain;
using Hsd.Users.Domain;

namespace Hsd.Users.Aplication.Search
{
    public interface ISearchUserService
    {
        Task<PageResult<User>> SearchAsync(UserFilter filter);
    }
}