using Aseme.Shared.Infrastructure.PubSub.Publisher;
using Microsoft.Extensions.Logging;

namespace Aseme.Shared.Infrastructure.PubSub.Subscriber
{
    public class PubSubSubscriber : IPubSubSubscriber
    {
        public string TopicName { get; set; }

        private readonly ILogger<PubSubSubscriber> _logger;

        private readonly IPubSubPublisher _publisher;

        public PubSubSubscriber(ILogger<PubSubSubscriber> logger, IPubSubPublisher publisher, string topicName)
        {
            _logger = logger;
            _publisher = publisher;

            // Topic information
            TopicName = topicName;
        }

        public virtual void Subscribe()
        {
            _publisher.Subscribe(TopicName, this);
        }

        public virtual void Unsubscribe()
        {
            _publisher.Unsubscribe(TopicName, this);
        }

        public virtual void Receive(string data)
        {
            _logger.LogTrace($"Received message in topic '{TopicName}` with data '{data}'");
        }
    }
}