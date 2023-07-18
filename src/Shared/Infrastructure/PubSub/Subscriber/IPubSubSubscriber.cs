namespace Aseme.Shared.Infrastructure.PubSub.Subscriber
{
    public interface IPubSubSubscriber
    {
        void Subscribe();

        void Unsubscribe();

        void Receive(string data);
    }
}