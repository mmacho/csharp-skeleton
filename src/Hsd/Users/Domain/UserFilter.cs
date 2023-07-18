using Aseme.Shared.Infrastructure.Http;

namespace Hsd.Users.Domain
{
    public class UserFilter : PaginateFilter
    {
        public string Distributor { get; set; }
    }
}