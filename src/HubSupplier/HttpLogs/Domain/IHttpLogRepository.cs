using Aseme.Shared.Infrastructure.Persistence.EntityFramework;

namespace Aseme.HubSupplier.HttpLogs.Domain
{
    public interface IHttpLogRepository : ILegacyRepository<HttpLog>
    {
    }
}