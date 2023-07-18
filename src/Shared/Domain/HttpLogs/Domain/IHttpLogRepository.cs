using Aseme.Shared.Infrastructure.Persistence.EntityFramework;

namespace Aseme.Shared.Domain.HttpLogs.Domain
{
    public interface IHttpLogRepository : ILegacyRepository<HttpLog>
    {
    }
}