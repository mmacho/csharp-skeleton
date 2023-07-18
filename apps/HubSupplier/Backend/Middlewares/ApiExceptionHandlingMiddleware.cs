using Aseme.Shared.Domain;
using Aseme.Shared.Infrastructure.Http.Response;
using Aseme.Shared.Infrastructure.Http.Response.Error;
using System.Net;
using System.Net.Mime;
using System.Text.Json;

namespace Aseme.Apps.HubSupplier.Backend.Middlewares
{
    public class ApiExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<ApiExceptionHandlingMiddleware> _logger;

        public ApiExceptionHandlingMiddleware(RequestDelegate next, ILogger<ApiExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpResponse response = context.Response;
            string? content = context.Request.ContentType;
            //TODO: Revisar cuando el API soporte XML
            response.ContentType = MediaTypeNames.Application.Json;
            _logger.LogError(exception.Message);
            CustomProblemDetails problemDetails;
            ErrorResponse<CustomProblemDetails> errorResponse = await ErrorResponse<CustomProblemDetails>.ReturnErrorAsync();
            switch (exception)
            {
                case UnauthorizedException e:
                    problemDetails = new CustomProblemDetails(new() { Code = e.Code, Message = e.Message })
                    {
                        Type = "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1",
                        Title = "Unauthorized",
                        Status = (int)HttpStatusCode.Unauthorized,
                        Instance = context.Request.Path,
                    };
                    errorResponse.Error = problemDetails;
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    break;

                case AccessDeniedException e:
                    problemDetails = new CustomProblemDetails(new() { Code = e.Code, Message = e.Message })
                    {
                        Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.3",
                        Title = "Forbidden",
                        Status = (int)HttpStatusCode.Forbidden,
                        Instance = context.Request.Path,
                    };
                    errorResponse.Error = problemDetails;
                    response.StatusCode = (int)HttpStatusCode.Forbidden;
                    break;

                case NotFoundException e:
                    problemDetails = new CustomProblemDetails(new() { Code = e.Code, Message = e.Message })
                    {
                        Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                        Title = "Not Found",
                        Status = (int)HttpStatusCode.NotFound,
                        Instance = context.Request.Path,
                    };
                    errorResponse.Error = problemDetails;
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;

                case DomainException e:
                    problemDetails = new CustomProblemDetails(new() { Code = e.Code, Message = e.Message })
                    {
                        Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                        Title = "Not Found",
                        Status = (int)HttpStatusCode.NotFound,
                        Instance = context.Request.Path,
                    };
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                case StaleStateIdentifiedException e:
                    problemDetails = new CustomProblemDetails(new() { Code = e.Code, Message = e.Message })
                    {
                        Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                        Title = "Conflict",
                        Status = (int)HttpStatusCode.Conflict,
                        Instance = context.Request.Path,
                    };
                    response.StatusCode = (int)HttpStatusCode.Conflict;
                    break;

                default:
                    problemDetails = new CustomProblemDetails(new() { Code = "GENERIC_ERROR", Message = "Generic Error" })
                    {
                        Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                        Title = "Internal Server Error.",
                        Status = (int)HttpStatusCode.InternalServerError,
                        Instance = context.Request.Path,
                        Detail = "Internal Server Error!"
                    };
                    _logger.LogError(exception, $"An unhandled exception has occurred, {exception.Message}");
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }
            errorResponse.Error = problemDetails;
            string result = JsonSerializer.Serialize(errorResponse);
            await response.WriteAsync(result);
        }
    }
}