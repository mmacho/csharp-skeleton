using System.ComponentModel.DataAnnotations;

namespace Aseme.Shared.Infrastructure.Http.Response
{
    public abstract class AbstractBaseResponse
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public byte[] Version { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime LastModifiedDate { get; set; }
    }
}
