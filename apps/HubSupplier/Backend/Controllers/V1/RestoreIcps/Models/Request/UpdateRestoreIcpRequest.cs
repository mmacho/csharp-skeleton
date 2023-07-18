using Aseme.HubSupplier.Shared.Domain.Operation;
using Aseme.Shared.Infrastructure.Http.Request;
using System.ComponentModel.DataAnnotations;

namespace Aseme.Apps.HubSupplier.Backend.Controllers.V1.RestoreIcps.Models.Request
{
    public class UpdateRestoreIcpRequest : UpdateBaseRequest
    {
        [StringLength(22)]
        [Required(ErrorMessage = "SupplyPoint is required")]
        [RegularExpression("[A-Z0-9]{1,22}")]
        public string SupplyPoint { get; set; }

        [StringLength(15)]
        public string? SerialNumber { get; set; }

        [Required(ErrorMessage = "Distributor is required")]
        [StringLength(6)]
        public string Distributor { get; set; }

        [Required(ErrorMessage = "OperationStatus is required")]
        public OperationStatusType OperationStatus { get; set; }

        public UpdateRestoreIcpDetailsRequest? RestoreIcpDetails { get; set; }


    }
}
