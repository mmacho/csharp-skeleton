using Aseme.Shared.Domain.Support;

namespace Aseme.HubSupplier.Shared.Domain.Operation
{
    public abstract class BaseOperation : AuditableEntity
    {
        public const string TableName = "BaseOperation";

        public string Distributor { get; set; }
    }
}