using Aseme.Shared.Domain.PubSub.Base;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Aseme.Shared.Infrastructure.Utils
{
    public static class PubSubUtils
    {
        public static PubSubMessage? GetPubSubMessage(string data, ILogger logger)
        {
            PubSubMessage? pubSubMessage = JsonConvert.DeserializeObject<PubSubMessage>(data);

            if (pubSubMessage == null)
            {
                logger.LogError($"Bad message received from publisher");
                return null;
            }

            return pubSubMessage;
        }
    }
}