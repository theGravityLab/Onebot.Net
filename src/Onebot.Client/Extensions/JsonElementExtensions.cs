using System.Text.Json;

namespace Onebot.Protocol.Extensions;

public static class JsonElementExtensions
{
    public static T GetObject<T>(this JsonElement element, JsonSerializerOptions options = null)
    {
        return JsonSerializer.Deserialize<T>(element.GetRawText(), options ?? new JsonSerializerOptions());
    }
}