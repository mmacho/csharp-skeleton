namespace Aseme.HubSupplier.Shared.Infrastructure.Settings
{
    public class JwtSettings
    {
        public string IssuerSigningKey { get; set; }

        public string ValidIssuer { get; set; }

        public string ValidAudience { get; set; }

        public bool RequireSignedTokens { get; set; }

        public bool RequireExpirationTime { get; set; }

        public bool RequireAudience { get; set; }

        public bool ValidateIssuer { get; set; }

        public bool ValidateAudience { get; set; }

        public bool ValidateLifetime { get; set; }
    }
}