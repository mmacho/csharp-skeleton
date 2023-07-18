using Aseme.Shared.Domain;

namespace Hsd.Users.Domain
{
    public class UserFilter : PaginateFilter
    {
        public string Distributor { get; set; }
    }
}