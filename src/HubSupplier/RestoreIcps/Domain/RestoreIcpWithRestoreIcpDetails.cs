using Aseme.Shared.Infrastructure.Persistence.Specifications.Model;

namespace Aseme.HubSupplier.RestoreIcps.Domain
{
    public class RestoreIcpWithRestoreIcpDetails : Specification<RestoreIcp>
    {
        public RestoreIcpWithRestoreIcpDetails()
        {
            AddInclude(r => r.RestoreIcpDetails);
        }

        public RestoreIcpWithRestoreIcpDetails(long id) : base(x => x.Id.Equals(id))
        {
            AddInclude(r => r.RestoreIcpDetails);
        }

        public RestoreIcpWithRestoreIcpDetails(RestoreIcpFilter filter) : base(x => x.OperationStatus.Equals(filter.OperationStatus))
        {
            AddInclude(r => r.RestoreIcpDetails);
        }
    }
}
