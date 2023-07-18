using Aseme.Shared.Domain.HttpLogs.Domain;
using Microsoft.AspNetCore.Http;

namespace Aseme.Shared.Infrastructure.Services.Logging
{
    public interface ILoggingService
    {
        Task<HttpLog> CreateHttpLog(HttpContext httpContext, RequestDelegate next);
    }
}