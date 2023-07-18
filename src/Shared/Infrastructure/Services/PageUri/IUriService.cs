using Aseme.Shared.Infrastructure.Http;

namespace Aseme.Shared.Infrastructure.Services.PageUri
{
    public interface IUriService
    {
        Uri GetPageUri(string? paramsFiltered, PaginateFilter filter, string? route);
    }
}
