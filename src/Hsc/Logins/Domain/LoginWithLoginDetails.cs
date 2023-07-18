using Aseme.Shared.Infrastructure.Persistence.Specifications.Model;

namespace Hsc.Logins.Domain
{
    public class LoginWithLoginDetails : Specification<Login>
    {
        public LoginWithLoginDetails()
        {
            AddInclude(r => r.Roles);
        }

        public LoginWithLoginDetails(int id) : base(x => x.Id.Equals(id))
        {
            AddInclude(r => r.Roles);
        }

        public LoginWithLoginDetails(LoginFilter filter) : base(x => x.UserName.Equals(filter.UserName))
        {
            AddInclude(r => r.Roles);
        }
    }
}