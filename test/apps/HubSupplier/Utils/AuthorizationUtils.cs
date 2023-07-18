using Aseme.HubSupplier.Shared.Infrastructure.Constants;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HubSupplierTest.apps.Utils
{
    internal static class AuthorizationUtils
    {
        public static string GeneratePortalJwt(string key, string? user, bool hasExpiration)
        {
            var issuerSigningKeyBytes = Encoding.UTF8.GetBytes(key);

            Claim[]? claims;

            if (user == null)
            {
                claims = new Claim[]
                {
                    new Claim("ModoFiltradoTerceros", "1")
                };
            }
            else
            {
                claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name, user),
                    new Claim("ModoFiltradoTerceros", "1")
                };
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            // Expiration
            DateTime? expirationDateTime = null;

            if (hasExpiration)
            {
                expirationDateTime = DateTime.UtcNow.AddSeconds(86400);
            }
            else
            {
                tokenHandler.SetDefaultTimesOnTokenCreation = false;
            }

            // Token creation
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expirationDateTime,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(issuerSigningKeyBytes), SecurityAlgorithms.HmacSha256Signature),
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }

        public static string GenerateDistributorJwt(string key, string? distributor)
        {
            var issuerSigningKeyBytes = Encoding.UTF8.GetBytes(key);

            Claim[]? claims;

            if (distributor == null)
            {
                claims = new Claim[]
                {
                    new Claim("ModoFiltradoTerceros", "1")
                };
            }
            else
            {
                claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name, distributor),
                    new Claim(AuthenticationConstants.SECURITY_TOKEN_DISTRIBUTOR_CLAIM, distributor),
                    new Claim("ModoFiltradoTerceros", "1")
                };
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddSeconds(86400),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(issuerSigningKeyBytes), SecurityAlgorithms.HmacSha256Signature),
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }
    }
}