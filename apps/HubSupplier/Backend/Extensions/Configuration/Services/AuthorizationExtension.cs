using Aseme.HubSupplier.Shared.Infrastructure.Constants;
using Microsoft.AspNetCore.Authorization;

namespace Aseme.Apps.HubSupplier.Backend.Extensions.Configuration.Services
{
    public static partial class AuthorizationExtension
    {
        public static IServiceCollection ConfigureAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                options.AddPolicy(AuthorizationConstants.PORTAL_POLICY, policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireAssertion(context =>
                    {
                        return context.User.FindFirst(AuthenticationConstants.SECURITY_TOKEN_DISTRIBUTOR_CLAIM) == null;
                    });
                });
            });

            return services;
        }
    }
}