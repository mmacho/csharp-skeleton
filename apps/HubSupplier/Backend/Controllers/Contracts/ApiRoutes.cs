namespace Aseme.Apps.HubSupplier.Backend.Controllers.Contracts
{
    /// <summary>
    /// 
    /// </summary>
    public static class ApiRoutes
    {
        /// <summary>
        /// 
        /// </summary>
        public const string BasePath = "api/";

        /// <summary>
        /// 
        /// </summary>
        public const string ApiVersion = "v{version:apiVersion}/";

        /// <summary>
        /// 
        /// </summary>
        public const string ApiVersion_V1_0 = "1";

        /// <summary>
        /// 
        /// </summary>
        public const string Id = "{id}";

        /// <summary>
        /// 
        /// </summary>
        public static class RestoreIcps
        {

            public const string Endpoint = "RestoreIcps";

        }

        /// <summary>
        /// 
        /// </summary>
        public static class OnlineMeters
        {
            public const string Endpoint = "OnlineMeters";


        }
    }
}
