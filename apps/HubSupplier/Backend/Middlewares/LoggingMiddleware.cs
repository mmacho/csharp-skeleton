using Aseme.Apps.HubSupplier.Backend.Utils;
using Aseme.HubSupplier.HttpLogs.Domain;
using Aseme.HubSupplier.Shared.Infrastructure.Services.Logging;
using Aseme.HubSupplier.Shared.Infrastructure.Settings;
using Aseme.Shared.Infrastructure.PubSub.Publisher;
using Microsoft.Extensions.Options;

namespace Aseme.Apps.HubSupplier.Backend.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly HttpLoggerSettings _options;

        private readonly ILoggingService _loggingService;

        //TODO: revisar loggingService
        public LoggingMiddleware(RequestDelegate next, IOptions<HttpLoggerSettings> options, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _options = options.Value;
            //_loggingService = loggingService;
        }

        public async Task Invoke(HttpContext httpContext, IPubSubPublisher pubSubPublisher)
        {
            if (_options == null || _options.IsEnabled == false)
            {
                await _next(httpContext);
                return;
            }

            bool isUnauthenticatedPath = RequestUtils.HasApiBasePath(httpContext) == false;

            if (isUnauthenticatedPath)
            {
                await _next(httpContext);
                return;
            }

            HttpLog httpLog = await _loggingService.CreateHttpLog(httpContext, _next);

            HttpLogWasReceivedMessage httpLogWasReceivedMessage = new(httpLog);
            pubSubPublisher.Publish(httpLogWasReceivedMessage);
        }
    }
}