namespace Aseme.HubSupplier.Shared.Infrastructure.Startup
{
    public interface IRestoreIcpWasUpdatedTopicService
    {
        void Receive(string data);
        void Subscribe();
        void Unsubscribe();
    }
}