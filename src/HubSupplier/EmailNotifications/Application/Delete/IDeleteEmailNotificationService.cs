namespace Aseme.HubSupplier.EmailNotifications.Application.Delete
{
    public interface IDeleteEmailNotificationService
    {
        Task DeleteAsync(long id);
    }
}