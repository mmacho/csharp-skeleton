using Newtonsoft.Json;

namespace Aseme.Shared.Domain.Support
{
    public abstract class BaseEntity
    {
        [JsonProperty("Id")]
        public long Id { get; private set; }

        public byte[] Version { get; set; }
    }
}