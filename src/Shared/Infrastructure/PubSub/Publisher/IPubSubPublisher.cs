using Aseme.Shared.Domain.PubSub;
using Aseme.Shared.Infrastructure.PubSub.Subscriber;

namespace Aseme.Shared.Infrastructure.PubSub.Publisher
{
    public interface IPubSubPublisher
    {
        void Publish(IPubSubMessage pubSubMessage);

        void Subscribe(string topic, IPubSubSubscriber subscriber);

        void Unsubscribe(string topic, IPubSubSubscriber subscriber);
    }
}