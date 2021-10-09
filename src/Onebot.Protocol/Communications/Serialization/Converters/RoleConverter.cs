using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Onebot.Protocol.Models.Relations;

namespace Onebot.Protocol.Communications.Serialization.Converters
{
    public class RoleConverter: JsonConverter<Role>
    {
        public override Role Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.GetString() switch
            {
                "admin" => Role.Administrator,
                "owner" => Role.Owner,
                "member" => Role.Member
            };
        }

        public override void Write(Utf8JsonWriter writer, Role value, JsonSerializerOptions options)
        {
            writer.WriteString("role", value switch
            {
                Role.Member => "member",
                Role.Administrator => "admin",
                Role.Owner => "owner"
            });
        }
    }
}