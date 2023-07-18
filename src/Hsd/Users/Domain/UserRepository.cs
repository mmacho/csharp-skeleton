using Aseme.Shared.Infrastructure.Persistence.EntityFramework;
using Hsd.Shared.Infrastructure.Persistence.EntityFramework;

namespace Hsd.Users.Domain
{
    public class UserRepository : LegacyRepository<User>, IUserRepository
    {
        public UserRepository(HsdDbContext context) : base(context)
        {
        }
    }
}