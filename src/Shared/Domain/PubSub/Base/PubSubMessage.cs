using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Aseme.Shared.Domain.PubSub.Base
{
    [Serializable]
    public class PubSubMessage : IPubSubMessage
    {
        public DateTime CreatedDateTime { get; private set; }
        public string UUID { get; private set; }
        public string Topic { get; set; }
        public JObject Payload { get; private set; }

        [JsonIgnore]
        public string Destination { get; set; }

        public PubSubMessage(string topic, object payload)
        {
            CreatedDateTime = DateTime.Now;
            UUID = Guid.NewGuid().ToString();
            Destination = topic;
            Topic = topic;
            Payload = JObject.FromObject(payload);
        }
    }
}