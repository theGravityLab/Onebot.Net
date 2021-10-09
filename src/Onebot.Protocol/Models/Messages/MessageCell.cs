using System.Collections;
using System.Collections.Generic;

namespace Onebot.Protocol.Models.Messages
{
    public record MessageCell
    {
        public string Type { get; set; }
        public IDictionary<string, object> Data { get; set; }

        public T GetData<T>(string key) => (T)Data[key];
        public void SetData(string key, object data) => Data[key] = data;
    }
}