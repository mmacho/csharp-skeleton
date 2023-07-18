namespace Aseme.Shared.Domain.Exceptions
{
    public class DomainException : BaseException
    {
        public DomainException(string code = null, string message = null) : base(code, message)
        {
        }
    }
}
