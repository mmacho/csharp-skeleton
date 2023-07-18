using Aseme.HubSupplier.Shared.Infrastructure.Persistence.EntityFramework;
using Aseme.Shared.Infrastructure.Persistence.EntityFramework;

namespace Aseme.Shared.Domain.HttpLogs.Domain
{
    //TODO: DEBE DESAPARECER CON HEXGONAL
    public class HttpLogRepository : LegacyRepository<HttpLog>, IHttpLogRepository
    {
        public HttpLogRepository(HubSuppliersDbContext context) : base(context)
        {
        }
    }
}