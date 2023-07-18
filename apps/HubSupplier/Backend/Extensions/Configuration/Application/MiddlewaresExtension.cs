using Aseme.Apps.HubSupplier.Backend.Middlewares;

namespace Aseme.Apps.HubSupplier.Backend.Extensions.Configuration.Application
{
    public static partial class MiddlewaresExtension
    {
        public static WebApplication ConfigureMiddlewares(this WebApplication app)
        {
            app.UseMiddleware<LoggingMiddleware>();

            // manage custom exceptions
            app.UseMiddleware<ApiExceptionHandlingMiddleware>();

            // internal middleware
            app.UseAuthentication();

            // internal middleware
            app.UseAuthorization();

            // custom middleware for authorization
            app.UseMiddleware<AuthorizationMiddleware>();

            return app;
        }
    }
}