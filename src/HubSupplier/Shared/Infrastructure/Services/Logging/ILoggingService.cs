using Aseme.HubSupplier.HttpLogs.Domain;
using Microsoft.AspNetCore.Http;

namespace Aseme.HubSupplier.Shared.Infrastructure.Services.Logging
{
    public interface ILoggingService
    {
        Task<HttpLog> CreateHttpLog(HttpContext httpContext, RequestDelegate next);
    }
}