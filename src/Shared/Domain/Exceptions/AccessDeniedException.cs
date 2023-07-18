namespace Aseme.Shared.Domain.Exceptions
{
    public class AccessDeniedException : BaseException
    {
        public AccessDeniedException(string code = null, string message = null) : base(code, message)
        {
        }

        public AccessDeniedException(string message) : base(message)
        {
        }
    }
}