using Aseme.Shared.Infrastructure.Persistence.EntityFramework;

namespace Hsd.Users.Domain
{
    public interface IUserRepository : ILegacyRepository<User>
    {
    }
}