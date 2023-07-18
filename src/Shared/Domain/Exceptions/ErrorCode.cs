namespace Aseme.Shared.Domain.Exceptions
{
    //TODO: i18n
    public static class ErrorCode
    {
        public const string UNAUTHORIZED = "ERR_001";

        public const string INVALID_TOKEN = "ERR_002";

        public const string NOT_FOUND = "ERR_003";

        public const string ALREADY_EXISTS = "ERR_004";

        public const string CONFLICT = "ERR_005";
    }
}