using Aseme.HubSupplier.Shared.Domain.Operation;
using Aseme.Shared.Infrastructure.Http.Response;
using System.ComponentModel.DataAnnotations;

namespace Aseme.Apps.HubSupplier.Backend.Models.V1.RestoreIcp
{
    public class RestoreIcpResponse : AbstractBaseResponse
    {
        [StringLength(22)]
        [Required(ErrorMessage = "SupplyPoint is required")]
        [RegularExpression("[A-Z0-9]{1,22}")]
        public string SupplyPoint { get; set; }

        [StringLength(15)]
        public string? SerialNumber { get; set; }

        [StringLength(6)]
        [Required(ErrorMessage = "Distributor is required")]
        public string Distributor { get; set; }

        [Required(ErrorMessage = "OperationStatus is required")]
        public OperationStatusType OperationStatus { get; set; }


        public RestoreIcpDetailsResponse? RestoreIcpDetails;


    }
}
