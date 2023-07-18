namespace Aseme.HubSupplier.Shared.Infrastructure.Constants
{
    public static class AuthorizationConstants
    {
        // Policies

        public const string PORTAL_POLICY = "Portal";
        public const string DISTRIBUTOR_POLICY = "Distributor";

        // Functionalities

        public const long HUB_ACCESS_FUNCTIONALITY_IDENTIFIER = 3;

        // Messages

        public const string USER_NOT_FOUND_MESSAGE = "User not found in database";
        public const string USER_NOT_AUTHORIZED = "User not authorized for this functionality";
    }
}