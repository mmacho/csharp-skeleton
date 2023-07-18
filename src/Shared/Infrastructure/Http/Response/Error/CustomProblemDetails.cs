using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace Aseme.Shared.Infrastructure.Http.Response.Error
{
    public class CustomProblemDetails : ProblemDetails
    {
        [JsonPropertyName("error")]
        public ValidationError Error { get; set; }

        public CustomProblemDetails()
        {
        }

        public CustomProblemDetails(ValidationError error)
        {
            Error = error;
        }
    }
}
