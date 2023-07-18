using Aseme.HubSupplier.EmailNotifications.Domain;
using Aseme.Shared.Domain.Support;

namespace Aseme.HubSupplier.EmailNotifications.Application.Search
{
    public class SearchEmailNotificationService : ISearchEmailNotificationService
    {
        private readonly IEmailNotificationRepository _repository;

        public SearchEmailNotificationService(IEmailNotificationRepository repository)
        {
            _repository = repository;
        }

        public async Task<PageResult<EmailNotification>> SearchAsync(EmailNotificationFilter filter)
        {
            EmailNotificationWithEmailNotificationDetails specification = new(filter);
            if (filter.PageNumber != null && filter.PageSize != null) { specification.ApplyPaging((int)filter.PageNumber, (int)filter.PageSize); }
            return await _repository.SearchAsync(specification);
        }
    }
}