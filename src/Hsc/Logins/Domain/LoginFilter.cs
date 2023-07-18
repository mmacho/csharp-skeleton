using Aseme.Shared.Domain;

namespace Hsc.Logins.Domain
{
    public class LoginFilter : PaginateFilter
    {
        public string UserName { get; set; }
    }
}