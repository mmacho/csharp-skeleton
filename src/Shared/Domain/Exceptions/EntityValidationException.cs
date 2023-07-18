using System.Data.Entity.Validation;

namespace Aseme.Shared.Domain.Exceptions
{
    public class EntityValidationException : Exception
    {
        public EntityValidationException()
               : base("One or more entity validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public EntityValidationException(string message) : base(message)
        {

        }

        public EntityValidationException(string message, Exception ex) : base(message, ex)
        {

        }
        public EntityValidationException(DbEntityValidationException dbException) : base(null, dbException)
        {
            foreach (var validationError in dbException.EntityValidationErrors)
            {
                Errors = validationError.ValidationErrors
                    .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                    .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
            }
        }

        public IDictionary<string, string[]> Errors { get; }
    }
}
