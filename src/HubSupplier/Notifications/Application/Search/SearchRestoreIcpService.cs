using Aseme.HubSupplier.Notifications.Domain;
using Aseme.Shared.Domain.Support;

namespace Aseme.HubSupplier.Notifications.Application.Search
{
    public class SearchNotificationService : ISearchNotificationService
    {
        private readonly INotificationRepository _repository;

        public SearchNotificationService(INotificationRepository repository)
        {
            _repository = repository;
        }

        public async Task<PageResult<BaseNotification>> SearchAsync(NotificationFilter filter)
        {
            NotificationWithNotificationDetails specification = new(filter);
            if (filter.PageNumber != null && filter.PageSize != null) { specification.ApplyPaging((int)filter.PageNumber, (int)filter.PageSize); }
            return await _repository.SearchAsync(specification);
        }
    }
}