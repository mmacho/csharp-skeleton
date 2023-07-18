namespace Aseme.HubSupplier.Shared.Infrastructure.Startup
{
    public interface IHttpLogReceivedTopicService
    {
        void Subscribe();

        void Unsubscribe();

        void Receive(string data);
    }
}