using Aseme.HubSupplier.Shared.Infrastructure.Constants;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Aseme.HubSupplier.Shared.Infrastructure.Providers.Claims
{
    public class ClaimsProvider : IClaimsProvider
    {
        public string? OwnerId { get; private set; }
        public string? DistributorId { get; private set; }

        public ClaimsProvider(IHttpContextAccessor accessor)
        {
            HttpContext? httpContext = accessor.HttpContext;

            string? uniqueName = httpContext?.User.Claims.SingleOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
            string? distributorId = httpContext?.User.Claims.SingleOrDefault(claim => claim.Type == AuthenticationConstants.SECURITY_TOKEN_DISTRIBUTOR_CLAIM)?.Value;

            OwnerId = string.IsNullOrEmpty(distributorId) ? uniqueName : null;
            DistributorId = distributorId;
        }
    }
}