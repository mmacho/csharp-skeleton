using Aseme.Shared.Infrastructure.Persistence.Specifications.Model;

namespace Hsd.Users.Domain
{
    public class UserWithUserDetails : Specification<User>
    {
        public UserWithUserDetails()
        {
        }

        public UserWithUserDetails(string name) : base(x => x.Name.Equals(name))
        {
        }

        public UserWithUserDetails(UserFilter filter) : base(x => x.Distributor.Equals(filter.Distributor))
        {
        }
    }
}