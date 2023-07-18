using Aseme.Shared.Domain;
using Aseme.Shared.Infrastructure.Http.Response;
using Aseme.Shared.Infrastructure.Services.PageUri;
using AutoMapper;
using Microsoft.AspNetCore.Http.Extensions;

namespace Aseme.Apps.HubSupplier.Backend.Extensions
{
    public static class MapperExtensions
    {
        public static PagedResponse<TDto> ToPagedResponse<T, TDto>(this PageResult<T> entityPage, PaginateFilter filter, IMapper mapper, IUriService uriService, string? route, IQueryCollection query)
            where T : class
        {
            var items = query.SelectMany(x => x.Value, (col, value) => new KeyValuePair<string, string>(col.Key, value)).ToList();
            items.RemoveAll(x => x.Key == "PageNumber" || x.Key == "PageSize");
            string paramsFiltered = new QueryBuilder(items).ToString();

            PagedResponse<TDto> pagedResponse = mapper.Map<PagedResponse<TDto>>(entityPage);

            pagedResponse.NextPage = pagedResponse.PageNumber >= 1 && pagedResponse.PageNumber < pagedResponse.TotalPages
            ? uriService.GetPageUri(paramsFiltered, new PaginateFilter()
            {
                PageNumber = pagedResponse.PageNumber + 1,
                PageSize = pagedResponse.PageSize
            }, route)
            : null;

            pagedResponse.PreviousPage = pagedResponse.PageNumber - 1 >= 1 && pagedResponse.PageNumber <= pagedResponse.TotalPages
            ? uriService.GetPageUri(paramsFiltered, new PaginateFilter()
            {
                PageNumber = pagedResponse.PageNumber - 1,
                PageSize = pagedResponse.PageSize
            }, route)
            : null;

            pagedResponse.FirstPage = uriService.GetPageUri(paramsFiltered, new PaginateFilter()
            {
                PageNumber = 1,
                PageSize = pagedResponse.PageSize
            }, route);

            pagedResponse.LastPage = uriService.GetPageUri(paramsFiltered, new PaginateFilter()
            {
                PageNumber = pagedResponse.TotalPages,
                PageSize = pagedResponse.PageSize
            }, route);
            return pagedResponse;
        }

    }
}
