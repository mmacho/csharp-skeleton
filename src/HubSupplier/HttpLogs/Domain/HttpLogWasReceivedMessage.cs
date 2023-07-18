using Aseme.Shared.Domain.PubSub.Base;

namespace Aseme.Shared.Domain.HttpLogs.Domain
{
    public class HttpLogWasReceivedMessage : PubSubMessage
    {
        public const string TOPIC_NAME = "HttpLog.WasReceived";

        public HttpLogWasReceivedMessage(object payload) : base(TOPIC_NAME, payload)
        {
        }
    }
}