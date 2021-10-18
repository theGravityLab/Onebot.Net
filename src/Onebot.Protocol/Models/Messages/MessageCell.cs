using System.Collections;
using System.Collections.Generic;

namespace Onebot.Protocol.Models.Messages
{
    public record MessageCell
    {
        public string Type { get; set; }
        public IDictionary<string, string> Data { get; set; }

        public static MessageCell From(string typeName, IEnumerable<KeyValuePair<string, string>> values) => new()
        {
            Type = typeName,
            Data = new Dictionary<string, string>(values)
        };
    }
}