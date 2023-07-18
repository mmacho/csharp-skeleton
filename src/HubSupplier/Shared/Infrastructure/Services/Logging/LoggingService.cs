using Aseme.Shared.Domain;
using Aseme.Shared.Domain.HttpLogs.Domain;
using Aseme.Shared.Infrastructure.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace Aseme.Shared.Infrastructure.Services.Logging
{
    public class LoggingService : ILoggingService
    {
        private readonly ILogger<ILoggingService> _logger;

        private const int TEXT_LIMIT = 4_000;

        public LoggingService(ILogger<ILoggingService> logger)
        {
            _logger = logger;
        }

        public async Task<HttpLog> CreateHttpLog(HttpContext httpContext, RequestDelegate next)
        {
            ConnectionInfo connectionInfo = httpContext.Connection;
            HttpRequest request = httpContext.Request;
            HttpResponse response = httpContext.Response;

            string ipAddress = connectionInfo.RemoteIpAddress?.ToString() ?? string.Empty;
            string? queryParams = request.QueryString.HasValue ? request.QueryString.Value : null;

            RequestData requestData = await GetRequestData(request, _logger);

            string? requestBody = requestData.Content;
            string? requestHeaders = LoggingUtils.GetHeadersAsString(request.Headers);

            ResponseData responseData = await GetResponseData(httpContext, next, _logger);

            string? responseBody = responseData.Content;
            string? responseHeaders = LoggingUtils.GetHeadersAsString(response.Headers);
            int executionTime = responseData.ExecutionTime;

            string scheme = request.Scheme;
            scheme = scheme.ToUpper();

            int statusCode = response.StatusCode;
            long? entityId = requestData.EntityId ?? responseData.EntityId;

            return new HttpLog()
            {
                ReceivedDateTime = DateTime.Now,
                IpAddress = ipAddress,
                Scheme = scheme,
                HttpMethod = request.Method,
                HttpPath = request.Path,
                HttpQueryParams = queryParams,
                HttpRequestHeaders = requestHeaders,
                HttpRequestBody = requestBody,
                HttpResponseHeaders = responseHeaders,
                HttpResponseBody = responseBody,
                HttpStatusCode = statusCode,
                ExecutionTime = executionTime,
                EntityId = entityId
            };
        }

        private static async Task<RequestData> GetRequestData(HttpRequest request, ILogger<ILoggingService> logger)
        {
            request.EnableBuffering();

            StreamReader streamReader = new(request.Body);
            string? requestContent = await streamReader.ReadToEndAsync();

            request.Body.Position = 0;

            requestContent = requestContent?.Trim();

            if (string.IsNullOrEmpty(requestContent))
            {
                return new RequestData { Content = null, EntityId = null };
            }

            long? entityId = null;

            try
            {
                dynamic? requestBody = JsonConvert.DeserializeObject(requestContent);
                entityId = requestBody?.id;

                // Remove formatting
                requestContent = JsonConvert.SerializeObject(requestBody, Formatting.None);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message + Environment.NewLine + ex.StackTrace);
            }

            requestContent = requestContent.Length > TEXT_LIMIT ? requestContent[..TEXT_LIMIT] : requestContent;

            return new RequestData { Content = requestContent, EntityId = entityId };
        }

        private static async Task<ResponseData> GetResponseData(HttpContext httpContext, RequestDelegate next, ILogger<ILoggingService> logger)
        {
            HttpResponse response = httpContext.Response;
            Stream originalResponseBody = response.Body;

            using MemoryStream memoryStream = new();
            response.Body = memoryStream;

            Stopwatch stopwatch = Stopwatch.StartNew();
            await next(httpContext);
            stopwatch.Stop();

            response.Body.Position = 0;

            using StreamReader streamReader = new(response.Body);
            string? responseContent = await streamReader.ReadToEndAsync();
            response.Body.Position = 0;

            await memoryStream.CopyToAsync(originalResponseBody);

            response.Body = originalResponseBody;

            responseContent = responseContent?.Trim();

            if (string.IsNullOrEmpty(responseContent))
            {
                return new ResponseData { Content = null, ExecutionTime = stopwatch.Elapsed.Milliseconds, EntityId = null };
            }

            long? entityId = null;

            try
            {
                dynamic? responseBody = JsonConvert.DeserializeObject(responseContent);

                if (responseBody?.data is JObject)
                    entityId = responseBody?.data?.id ?? entityId;

                responseContent = JsonConvert.SerializeObject(responseBody, Formatting.None);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message + Environment.NewLine + ex.StackTrace);
            }

            responseContent = responseContent?.Length > TEXT_LIMIT ? responseContent[..TEXT_LIMIT] : responseContent;

            return new ResponseData
            {
                Content = responseContent,
                ExecutionTime = stopwatch.Elapsed.Milliseconds,
                EntityId = entityId
            };
        }
    }
}