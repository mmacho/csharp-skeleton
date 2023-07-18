using Aseme.HubSupplier.Shared.Infrastructure.Persistence.EntityFramework;
using Aseme.Shared.Infrastructure.Persistence.EntityFramework;

namespace Aseme.HubSupplier.HttpLogs.Domain
{
    public class HttpLogRepository : LegacyRepository<HttpLog>, IHttpLogRepository
    {
        public HttpLogRepository(HubSuppliersDbContext context) : base(context)
        {
        }
    }
}