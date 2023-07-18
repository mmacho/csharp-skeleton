using Aseme.HubSupplier.RestoreIcps.Domain;
using System.ComponentModel.DataAnnotations;

namespace Aseme.Apps.HubSupplier.Backend.Controllers.V1.RestoreIcps.Models.Response
{
    public class RestoreIcpDetailsResponse
    {
        [Required(ErrorMessage = "RestoreIcpStatus is required")]
        public RestoreIcpStatusType RestoreIcpStatus { get; set; }

        [Required(ErrorMessage = "ExecutionDate is required")]
        public DateTime ExecutionDate { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(512)]
        public string Description { get; set; }
    }
}