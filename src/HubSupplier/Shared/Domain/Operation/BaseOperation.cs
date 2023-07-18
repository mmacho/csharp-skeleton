using Aseme.Shared.Domain;

namespace Aseme.HubSupplier.Shared.Domain.Operation
{
    //TODO: Esta clase debería tener el usuario que crear la operación y la distribuidora para ser capaz de filtrar los datos en función de quién lo consulte
    public abstract class BaseOperation : AuditableEntity
    {
        public const string TableName = "BaseOperation";

        public string Distributor { get; set; }
    }
}