using Aseme.Shared.Domain.PubSub.Base;
using Aseme.Shared.Infrastructure.PubSub.Subscriber;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Aseme.Shared.Infrastructure.PubSub.Publisher
{
    public class PubSubPublisher : IPubSubPublisher
    {
        private readonly ILogger<PubSubPublisher> _logger;
        private readonly Dictionary<string, List<IPubSubSubscriber>> subscribers;

        public PubSubPublisher(ILogger<PubSubPublisher> logger)
        {
            _logger = logger;
            subscribers = new Dictionary<string, List<IPubSubSubscriber>>();
        }

        public void Publish(IPubSubMessage message)
        {
            PublishRecursive(message);
        }

        private void PublishRecursive(IPubSubMessage message)
        {
            if (subscribers.ContainsKey(message.Destination))
            {
                string topic = message.Topic;
                string messageData = JsonConvert.SerializeObject(message);
                _logger.LogInformation($"Sent message to topic '{topic}' with data '{messageData}'");

                foreach (var subscriber in subscribers[message.Destination])
                {
                    subscriber.Receive(messageData);
                }
            }

            int lastDotIndex = message.Destination.LastIndexOf('.');
            if (lastDotIndex != -1)
            {
                string parentTopic = message.Destination[..lastDotIndex];
                message.Destination = parentTopic;
                PublishRecursive(message);
            }
        }

        public void Subscribe(string topic, IPubSubSubscriber subscriber)
        {
            if (subscribers.ContainsKey(topic) == false)
            {
                subscribers[topic] = new List<IPubSubSubscriber>();
            }

            subscribers[topic].Add(subscriber);
        }

        public void Unsubscribe(string topic, IPubSubSubscriber subscriber)
        {
            if (subscribers.ContainsKey(topic))
            {
                subscribers[topic].Remove(subscriber);
            }
        }
    }
}