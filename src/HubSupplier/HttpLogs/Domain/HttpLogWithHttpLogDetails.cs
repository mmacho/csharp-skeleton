using Aseme.Shared.Infrastructure.Persistence.Specifications.Model;

namespace Aseme.HubSupplier.HttpLogs.Domain
{
    public class HttpLogWithHttpLogDetails : Specification<HttpLog>
    {
        public HttpLogWithHttpLogDetails()
        {
        }

        public HttpLogWithHttpLogDetails(int id) : base(x => x.Id.Equals(id))
        {
        }

        public HttpLogWithHttpLogDetails(HttpLogFilter filter) : base(x => x.EntityId.Equals(filter.EntityId))
        {
        }
    }
}