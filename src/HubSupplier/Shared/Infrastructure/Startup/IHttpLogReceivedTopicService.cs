namespace Aseme.Shared.Domain.HttpLogs.Infrastructure.Received
{
    public interface IHttpLogReceivedTopicService
    {
        void Subscribe();

        void Unsubscribe();

        void Receive(string data);
    }
}