using Aseme.Shared.Domain;
using Hsc.Logins.Domain;

namespace Hsc.Logins.Aplication.Search
{
    public interface ISearchLoginService
    {
        Task<PageResult<Login>> SearchAsync(LoginFilter filter);
    }
}