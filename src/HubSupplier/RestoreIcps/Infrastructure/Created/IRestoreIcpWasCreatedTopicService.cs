namespace Aseme.HubSupplier.RestoreIcps.Infrastructure.Created
{
    public interface IRestoreIcpWasCreatedTopicService
    {
        void Subscribe();

        void Unsubscribe();

        void Receive(string data);
    }
}