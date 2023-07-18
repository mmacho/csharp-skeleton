namespace Aseme.Shared.Domain.Exceptions
{
    public abstract class BaseException : Exception
    {
        public string Code { get; }

        public BaseException(string code = null, string message = null)
             : base(message)
        {
            Code = code;
        }
    }
}
