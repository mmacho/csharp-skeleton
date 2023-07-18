using Aseme.HubSupplier.Shared.Domain.Operation;
using Aseme.Shared.Domain;

namespace Aseme.HubSupplier.RestoreIcps.Domain
{
    public class RestoreIcpFilter : PaginateFilter
    {
        public OperationStatusType OperationStatus { get; set; }

    }
}
