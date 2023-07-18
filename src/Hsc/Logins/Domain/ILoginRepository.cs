using Aseme.Shared.Infrastructure.Persistence.EntityFramework;

namespace Hsc.Logins.Domain
{
    public interface ILoginRepository : ILegacyRepository<Login>
    {
    }
}