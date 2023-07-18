namespace Aseme.Shared.Domain.HttpLogs.Application.Delete
{
    public interface IDeleteHttpLogService
    {
        Task DeleteAsync(int id);
    }
}