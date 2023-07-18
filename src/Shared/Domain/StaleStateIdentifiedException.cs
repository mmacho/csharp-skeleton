namespace Aseme.Shared.Domain
{
    public class StaleStateIdentifiedException : BaseException
    {

        public StaleStateIdentifiedException(string code = null, string message = null) : base(code, message)
        {

        }

        public StaleStateIdentifiedException(string message) : base(message)
        {

        }

        public StaleStateIdentifiedException(string code, string name, object key)
            : base(code, $"Entity \"{name}\" ({key}) was not updated.")
        {

        }
    }
}
