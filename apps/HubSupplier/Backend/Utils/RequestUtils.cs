using Aseme.Apps.HubSupplier.Backend.Controllers.Contracts;

namespace Aseme.Apps.HubSupplier.Backend.Utils
{
    public static class RequestUtils
    {
        public static bool HasApiBasePath(HttpContext httpContext)
        {
            var request = httpContext.Request;
            var path = request.Path;

            return path.StartsWithSegments(RequestConstants.API_BASE_PATH, StringComparison.OrdinalIgnoreCase);
        }

        public static bool HasSwaggerBasePath(HttpContext httpContext)
        {
            var request = httpContext.Request;
            var path = request.Path;

            return path.StartsWithSegments(RequestConstants.SWAGGER_BASE_PATH, StringComparison.OrdinalIgnoreCase);
        }

        public static bool IsSwaggerIndex(HttpContext httpContext)
        {
            var request = httpContext.Request;
            var path = request.Path;

            return path.Value.EndsWith(RequestConstants.SWAGGER_INDEX);
        }
    }
}