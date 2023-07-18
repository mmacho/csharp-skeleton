namespace Aseme.Shared.Domain.Exceptions
{
    public class UnauthorizedException : BaseException
    {
        public UnauthorizedException(string code = null, string message = null, string userName = null) : base(code, $"{message} ({userName})")
        {
        }

        public UnauthorizedException(string code = null, string message = null) : base(code, message)
        {
        }

        public UnauthorizedException(string message) : base(message)
        {
        }
    }
}