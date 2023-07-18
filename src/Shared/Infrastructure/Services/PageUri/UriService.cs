using Aseme.Shared.Domain;
using Microsoft.AspNetCore.WebUtilities;

namespace Aseme.Shared.Infrastructure.Services.PageUri
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;

        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetPageUri(string? paramsFiltered, PaginateFilter filter, string? route)
        {
            var _enpointUri = new Uri(string.Concat(_baseUri, route, paramsFiltered));
            var modifiedUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "PageNumber", filter.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "PageSize", filter.PageSize.ToString());
            return new Uri(modifiedUri);
        }
    }
}