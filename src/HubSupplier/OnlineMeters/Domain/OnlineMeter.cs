using Aseme.HubSupplier.Shared.Domain.Operation;

namespace Aseme.HubSupplier.OnlineMeters.Domain
{
    public class OnlineMeter : BaseOperation
    {
        public const string TableName = "OnlineMeter";

        public string SupplyPoint { get; set; }

        public OperationStatusType OperationStatus { get; set; }

        public virtual OnlineMeterDetails OnlineMeterDetails { get; set; }
    }
}