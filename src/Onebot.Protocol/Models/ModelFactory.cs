using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Onebot.Protocol.Models.Actions;
using Onebot.Protocol.Models.Events;
using Onebot.Protocol.Models.Events.Message;
using Onebot.Protocol.Models.Events.Meta;
using Onebot.Protocol.Models.Receipts;

namespace Onebot.Protocol.Models;

public static class ModelFactory
{
    private static readonly JsonSerializer serializer;
    
    private static readonly JsonSerializerSettings settings;

    static ModelFactory()
    {
        settings = new JsonSerializerSettings()
        {
            ContractResolver = new DefaultContractResolver()
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        };
        serializer = JsonSerializer.Create(settings);
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
            var evt = obj.ToObject(eventRegistry[key], serializer) as EventBase;
            return evt;
        }
        else
        {
            return new UnknownEvent();
        }
    }

    public static string SerializeAction(ActionBase action, string echo)
    {
        dynamic obj = new
        {
            action = action.Action,
            @params = action,
            echo
        };

        return JsonConvert.SerializeObject(obj, settings);
    }

    public static ReceiptBase ConstructReceipt(JObject obj, Type receipt)
    {
        return obj["data"].ToObject(receipt, serializer) as ReceiptBase;
    }
}