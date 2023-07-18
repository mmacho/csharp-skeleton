using Aseme.Shared.Domain.PubSub;

namespace Aseme.HubSupplier.RestoreIcps.Domain
{
    public class RestoreIcpWasUpdatedMessage : PubSubMessage
    {
        public const string TOPIC_NAME = "RestoreIcp.WasUpdated";

        public RestoreIcpWasUpdatedMessage(object payload) : base(TOPIC_NAME, payload)
        {
        }
    }
}