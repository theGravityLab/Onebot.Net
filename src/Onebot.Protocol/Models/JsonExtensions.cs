using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Onebot.Protocol.Models;

public static class JsonExtensions
{
    public static void Populate<T>(this JObject value, T target) where T : class
    {
        using (var sr = value.CreateReader())
        {
            JsonSerializer.CreateDefault().Populate(sr, target); // Uses the system default JsonSerializerSettings
        }
    }
}