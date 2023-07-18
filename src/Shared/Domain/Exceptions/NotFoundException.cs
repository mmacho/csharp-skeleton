namespace Aseme.Shared.Domain.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string code = null, string message = null) : base(code, message)
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string code, string name, object key)
            : base(code, $"Entity \"{name}\" ({key}) was not found")
        {
        }
    }
}