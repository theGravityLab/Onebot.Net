using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Onebot.Protocol.Models.Relations;

namespace Onebot.Protocol.Communications.Serialization.Converters
{
    public class GenderConverter: JsonConverter<Gender>
    {
        public override Gender Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.GetString() switch
            {
                "male" => Gender.Male,
                "female" => Gender.Female,
                "unknown" => Gender.Unknown
            };
        }

        public override void Write(Utf8JsonWriter writer, Gender value, JsonSerializerOptions options)
        {
            writer.WriteString("sex", value.ToString().ToLower());
        }
    }
}