using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Aseme.Shared.Infrastructure.Utils
{
    public static class JsonSerializerUtils
    {
        public static string ToJson(this object obj)
        {
            var token = JToken.FromObject(obj, new JsonSerializer() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            return token.ToString();
        }
    }
}