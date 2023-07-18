namespace Aseme.HubSupplier.RestoreIcps.Infrastructure.Updated
{
    public interface IRestoreIcpWasUpdatedTopicService
    {
        void Receive(string data);
        void Subscribe();
        void Unsubscribe();
    }
}