namespace Aseme.HubSupplier.Shared.Infrastructure.Constants
{
    public static class AuthenticationConstants
    {
        // Claims

        public const string SECURITY_TOKEN_UNIQUE_NAME_CLAIM = "unique_name";
        public const string SECURITY_TOKEN_DISTRIBUTOR_CLAIM = "distribuidora";

        // DEV ONLY!

        public const string SECURITY_TOKEN_NAME_PORTAL1 = "portal1";
        public const string SECURITY_TOKEN_NAME_DISTRIBUTOR1 = "0001";

        // Messages

        public const string AUTHORIZATION_HEADER_MISSING_MESSAGE = "HTTP Authorization header missing";
        public const string INVALID_TOKEN_MESSAGE = "Security token is invalid";
        public const string EXPIRED_TOKEN_MESSAGE = "Security token has expired";
        public const string IDENTITY_CLAIM_MISSING_MESSAGE = "Security token's claim \"unique_name\" or \"distributor\" missing";
        public const string USER_NOT_FOUND_MESSAGE = "User not found in database";
        public const string USER_NOT_AUTHORIZED = "User not authorized for this functionality";
    }
}