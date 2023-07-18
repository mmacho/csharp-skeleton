using System.ComponentModel.DataAnnotations;

namespace Aseme.Apps.HubSupplier.Backend.Domain.V1.RestoreIcp
{
    public class CreateRestoreIcpRequest
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
    }
}
