using Aseme.Apps.HubSupplier.Backend.Utils;
using Aseme.HubSupplier.Shared.Infrastructure.Constants;
using Aseme.HubSupplier.Shared.Infrastructure.Settings;
using Aseme.Shared.Domain.Exceptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Aseme.Apps.HubSupplier.Backend.Extensions.Configuration.Services
{
    public static partial class AuthenticationExtension
    {
        public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            JwtSettings jwtSettings = new();
            configuration.GetSection(OptionsExtension.JWT_SETTINGS_SECTION_KEY).Bind(jwtSettings);

            TokenValidationParameters tokenValidationParameters = AuthenticationUtils.GetTokenValidationParameters(jwtSettings);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = tokenValidationParameters;
                options.Events = GetJwtBearerEvents();
            });

            return services;
        }

        public static JwtBearerEvents GetJwtBearerEvents()
        {
            Func<JwtBearerChallengeContext, Task> jwtBearerChallengeContext = new(context =>
            {
                context.HandleResponse();

                return Task.FromException(new UnauthorizedException(ErrorCode.UNAUTHORIZED, AuthenticationConstants.AUTHORIZATION_HEADER_MISSING_MESSAGE));
            });

            Func<AuthenticationFailedContext, Task> authenticationFailedHandler = new(context =>
            {
                var exception = context.Exception;

                if (exception is SecurityTokenValidationException)
                {
                    if (exception is SecurityTokenExpiredException)
                    {
                        throw new AccessDeniedException(ErrorCode.INVALID_TOKEN, AuthenticationConstants.EXPIRED_TOKEN_MESSAGE);
                    }

                    throw new AccessDeniedException(ErrorCode.INVALID_TOKEN, AuthenticationConstants.INVALID_TOKEN_MESSAGE);
                }

                throw exception;
            });

            return new JwtBearerEvents
            {
                OnChallenge = jwtBearerChallengeContext,
                OnAuthenticationFailed = authenticationFailedHandler,
            };
        }
    }
}