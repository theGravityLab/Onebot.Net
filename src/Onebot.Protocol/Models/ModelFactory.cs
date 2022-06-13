using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Onebot.Protocol.Models.Events;
using Onebot.Protocol.Models.Events.Meta;

namespace Onebot.Protocol.Models;

public static class ModelFactory
{
    private static readonly Dictionary<string, Type> eventRegistry = new()
    {
        { "meta.heartbeat", typeof(HeartbeatEvent) }
    };
    
    public static EventBase ConstructEvent(JObject obj)
    {
        string type = obj.Value<string>("type");
        string detailedType = obj.Value<string>("detailed_type");
        string subType = obj.Value<string>("sub_type");

        string key = $"{type}.{detailedType}";
        if (eventRegistry.ContainsKey(key))
        {
            var evt = obj.ToObject(eventRegistry[key]) as EventBase;
            return evt;
        }
        else
        {
            return new UnknownEvent();
        }

    }
}