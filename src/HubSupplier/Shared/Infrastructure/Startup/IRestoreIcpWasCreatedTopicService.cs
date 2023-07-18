namespace Aseme.HubSupplier.Shared.Infrastructure.Startup
{
    public interface IRestoreIcpWasCreatedTopicService
    {
        void Subscribe();

        void Unsubscribe();

        void Receive(string data);
    }
}