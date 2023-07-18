namespace Aseme.Shared.Domain
{
    public class DomainException : BaseException
    {
        public DomainException(string code = null, string message = null) : base(code, message)
        {
        }
    }
}
