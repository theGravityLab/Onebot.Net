using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Onebot.Protocol.Models.Events;
using Onebot.Protocol.Models.Events.Message;
using Onebot.Protocol.Models.Events.Meta;

namespace Onebot.Protocol.Models;

public static class ModelFactory
{
    private static JsonSerializer serializer;

    static ModelFactory()
    {
        serializer = JsonSerializer.Create(new JsonSerializerSettings()
        {
            ContractResolver = new DefaultContractResolver()
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        });
    }
    
    private static readonly Dictionary<string, Type> eventRegistry = new()
    {
        { "meta.heartbeat", typeof(HeartbeatEvent) },
        { "message.group", typeof(GroupMessageEvent) },
        { "message.private", typeof(PrivateMessageEvent) }
    };

    public static EventBase ConstructEvent(JObject obj)
    {
        string type = obj.Value<string>("type");
        string detailedType = obj.Value<string>("detail_type");
        string subType = obj.Value<string>("sub_type");

        string key = $"{type}.{detailedType}";
        if (eventRegistry.ContainsKey(key))
        {
            var evt = obj.ToObject(eventRegistry[key],serializer) as EventBase;
            return evt;
        }
        else
        {
            return new UnknownEvent();
        }
    }
}