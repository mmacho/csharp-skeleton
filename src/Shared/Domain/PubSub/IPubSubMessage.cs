using Newtonsoft.Json.Linq;

namespace Aseme.Shared.Domain.PubSub
{
    public interface IPubSubMessage
    {
        DateTime CreatedDateTime { get; }
        string Topic { get; }
        JObject Payload { get; }
        string UUID { get; }

        string Destination { get; set; }
    }
}