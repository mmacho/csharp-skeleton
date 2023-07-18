namespace Aseme.HubSupplier.HttpLogs.Application.Delete
{
    public interface IDeleteHttpLogService
    {
        Task DeleteAsync(int id);
    }
}