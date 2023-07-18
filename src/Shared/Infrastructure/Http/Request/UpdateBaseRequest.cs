using System.ComponentModel.DataAnnotations;

namespace Aseme.Shared.Infrastructure.Http.Request
{
    public class UpdateBaseRequest
    {
        public long? Id { get; set; }

        [Required]
        public byte[] Version { get; set; }
    }
}
