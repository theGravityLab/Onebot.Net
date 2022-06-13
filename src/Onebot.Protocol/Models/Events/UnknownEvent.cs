using Newtonsoft.Json.Linq;

namespace Onebot.Protocol.Models.Events;

/// <summary>
/// 不在 Onebot 协议里的事件
/// </summary>
public record UnknownEvent: EventBase
{
    /// <summary>
    /// 事件原型
    /// </summary>
    public JObject RawObject { get; set; }
}