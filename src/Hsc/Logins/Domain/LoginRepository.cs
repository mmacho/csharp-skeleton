using Aseme.Shared.Infrastructure.Persistence.EntityFramework;
using Hsc.Shared.Infrastructure.Persistence.EntityFramework;

namespace Hsc.Logins.Domain
{
    public class LoginRepository : LegacyRepository<Login>, ILoginRepository
    {
        public LoginRepository(HscDbContext context) : base(context)
        {
        }
    }
}