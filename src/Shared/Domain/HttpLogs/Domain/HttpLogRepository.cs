using Aseme.Shared.Infrastructure.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Aseme.Shared.Domain.HttpLogs.Domain
{
    //TODO: DEBE DESAPARECER CON HEXGONAL
    public class HttpLogRepository : LegacyRepository<HttpLog>, IHttpLogRepository
    {
        public HttpLogRepository(DbContext context) : base(context)
        {
        }
    }
}