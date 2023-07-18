﻿using Aseme.Shared.Domain;

namespace Aseme.Shared.Infrastructure.Services.PageUri
{
    public interface IUriService
    {
        Uri GetPageUri(string? paramsFiltered, PaginateFilter filter, string? route);
    }
}
