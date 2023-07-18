using Aseme.Apps.HubSupplier.Backend.Settings;
using Aseme.HubSupplier.Shared.Infrastructure.Constants;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Aseme.Apps.HubSupplier.Backend.Utils
{
    public class AuthenticationUtils
    {
        public static string GeneratePortalJwt(JwtSettings options, string name)
        {
            var issuerSigningKeyBytes = Encoding.UTF8.GetBytes(options.IssuerSigningKey);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, name),
                    new Claim("ModoFiltradoTerceros", "1")
                }),
                Expires = DateTime.UtcNow.AddSeconds(86400),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(issuerSigningKeyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }

        public static string GenerateDistributorJwt(JwtSettings options, string distributor)
        {
            var issuerSigningKeyBytes = Encoding.UTF8.GetBytes(options.IssuerSigningKey);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, distributor),
                    new Claim(AuthenticationConstants.SECURITY_TOKEN_DISTRIBUTOR_CLAIM, distributor)
                }),
                Expires = DateTime.UtcNow.AddSeconds(86400),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(issuerSigningKeyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }

        public static TokenValidationParameters GetTokenValidationParameters(JwtSettings options)
        {
            var issuerSigningKey = options.IssuerSigningKey;
            var issuerSigningKeyBytes = Encoding.UTF8.GetBytes(issuerSigningKey);

            return new TokenValidationParameters()
            {
                IssuerSigningKey = new SymmetricSecurityKey(issuerSigningKeyBytes),

                ValidIssuer = options.ValidIssuer,
                ValidAudience = options.ValidAudience,

                RequireSignedTokens = options.RequireSignedTokens,
                RequireExpirationTime = options.RequireExpirationTime,
                RequireAudience = options.RequireAudience,

                ValidateLifetime = options.ValidateLifetime,
                ValidateAudience = options.ValidateAudience,
                ValidateIssuer = options.ValidateIssuer,
            };
        }
    }
}