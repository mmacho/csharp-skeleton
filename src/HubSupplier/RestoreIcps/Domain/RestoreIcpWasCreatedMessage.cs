
using Aseme.Shared.Domain.PubSub;

namespace Aseme.HubSupplier.RestoreIcps.Domain
{
    public class RestoreIcpWasCreatedMessage : PubSubMessage
    {
        public const string TOPIC_NAME = "RestoreIcp.WasCreated";

        public RestoreIcpWasCreatedMessage(object payload) : base(TOPIC_NAME, payload)
        {
        }
    }
}