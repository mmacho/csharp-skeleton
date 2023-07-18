using Microsoft.AspNetCore.Http;
using System.Text;

namespace Aseme.Shared.Infrastructure.Utils
{
    public static class LoggingUtils
    {
        public static string? GetHeadersAsString(IHeaderDictionary headers)
        {
            StringBuilder stringBuilder = new();

            var keys = headers.Keys;

            if (keys.Count == 0)
            {
                return null;
            }

            foreach (string key in keys)
            {
                var value = headers[key].ToString();
                value = value.Replace("\"", "\\\"");

                stringBuilder.Append("\"" + key + "\":\"" + value + "\",");
            }

            // Remove the trailing comma and whitespace
            return stringBuilder.ToString().TrimEnd(',', ' ');
        }
    }
}