using Aseme.Apps.HubSupplier.Backend.Utils;
using Aseme.HubSupplier.Shared.Infrastructure.Constants;
using Aseme.HubSupplier.Shared.Infrastructure.Persistence.EntityFramework;
using Aseme.HubSupplier.Shared.Infrastructure.Settings;
using Aseme.Shared.Domain.Exceptions;
using Hsc.Logins.Aplication.Search;
using Hsc.Logins.Domain;
using Hsc.Roles.Domain;
using Hsc.Shared.Infrastructure.Persistence.EntityFramework;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Aseme.Apps.HubSupplier.Backend.Middlewares
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<AuthorizationMiddleware> _logger;
        private readonly JwtSettings _options;

        public AuthorizationMiddleware(RequestDelegate next, ILogger<AuthorizationMiddleware> logger, IWebHostEnvironment environment, IOptions<JwtSettings> options)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
            _options = options.Value;
        }

        private bool ShouldBypassMiddleware(HttpContext httpContext)
        {
            if (_environment.IsDevelopment() && RequestUtils.HasSwaggerBasePath(httpContext))
            {
                if (RequestUtils.IsSwaggerIndex(httpContext))
                {
                    string portalJwt = AuthenticationUtils.GeneratePortalJwt(_options, AuthenticationConstants.SECURITY_TOKEN_NAME_PORTAL1);
                    _logger.LogInformation("JWT (Portal) {}", portalJwt);

                    string distributorJwt = AuthenticationUtils.GenerateDistributorJwt(_options, AuthenticationConstants.SECURITY_TOKEN_NAME_DISTRIBUTOR1);
                    _logger.LogInformation("JWT (Distributor) {}", distributorJwt);
                }

                return true;
            }

            return RequestUtils.HasApiBasePath(httpContext) ? false : true;
        }

        private static async Task<Login?> GetLogin(ISearchLoginService searchLoginService, string name)
        {
            LoginFilter userFilter = new()
            {
                UserName = name
            };

            var entity = await searchLoginService.SearchAsync(userFilter);
            var data = entity.Data;

            if (data.Count == 0)
            {
                return null;
            }

            return data[0];
        }

        public async Task Invoke(HttpContext httpContext, HscDbContext hscDbContext, ISearchLoginService searchLoginService, HubSuppliersDbContext applicationDbContext)
        {
            if (ShouldBypassMiddleware(httpContext))
            {
                await _next(httpContext);
                return;
            }

            ClaimsPrincipal claimsPrincipal = httpContext.User;
            IEnumerable<Claim> securityTokenClaims = claimsPrincipal.Claims;

            Claim? uniqueNameClaim = securityTokenClaims.SingleOrDefault(claim => claim.Type == ClaimTypes.Name);
            string? uniqueName = uniqueNameClaim?.Value;

            if (uniqueName == null)
            {
                throw new UnauthorizedException(ErrorCode.INVALID_TOKEN, AuthenticationConstants.IDENTITY_CLAIM_MISSING_MESSAGE);
            }

            // Get user
            Login? user = await GetLogin(searchLoginService, uniqueName);

            if (user == null)
            {
                throw new UnauthorizedException(ErrorCode.INVALID_TOKEN, AuthorizationConstants.USER_NOT_FOUND_MESSAGE, uniqueName);
            }

            // Get roles & functionalities
            RoleRepository roleRepository = new(hscDbContext);

            if (AuthorizationUtils.IsUserUnauthorized(user, roleRepository))
            {
                throw new UnauthorizedException(ErrorCode.UNAUTHORIZED, AuthorizationConstants.USER_NOT_AUTHORIZED);
            }

            // Map user
            applicationDbContext.User = user;

            await _next(httpContext);
        }
    }
}