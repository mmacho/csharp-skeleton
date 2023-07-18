namespace HubSupplierTest.apps.Constants
{
    public static class AuthorizationTestConstants
    {
        // Request

        public const string BEARER = "Bearer";

        // Unique names

        public const string MISSING_USERNAME = "missinguser";
        public const string UNAUTHORIZED_USERNAME = "noauth";
        public const string AUTHORIZED_USER_PORTAL1 = "portal1";
        public const string AUTHORIZED_USER_PORTAL2 = "portal2";
        public const string AUTHORIZED_USER_DISTRIBUTOR1 = "0001";
        public const string AUTHORIZED_USER_DISTRIBUTOR2 = "0002";

        // Roles

        public const string DATADAIS_ROLE = "Rol Datadis";
        public const string NETWORK_USER_PLATFORM_ROLE = "Rol Plataforma usuario de la red";

        // Functionalities

        public const string DISTRIBUTOR_DATA_FILTER_FUNCTIONALITY = "Filtrado para devolver datos de una distribuidora";
        public const string CONTRACT_API_DATA_FUNCTIONALITY = "Funcionalidad devolver datos API Contratos";
        public const string HUB_SUPPLIERS_FUNCTIONALITY = "Acceso HUB Suppliers";
    }
}