using Aseme.HubSupplier.Shared.Domain.Operation;
using Microsoft.EntityFrameworkCore;

namespace Aseme.HubSupplier.RestoreIcps.Domain
{
    [Owned]
    [Serializable]
    public class RestoreIcp : BaseOperation
    {
        public new const string TableName = "RestoreIcp";

        public string SupplyPoint { get; set; }

        public string? SerialNumber { get; set; }

        public OperationStatusType OperationStatus { get; set; }

        public virtual RestoreIcpDetails? RestoreIcpDetails { get; set; }
    }
}