namespace Onebot.Protocol.Models.Events;

/// <summary>
/// 当服务器或者自身关闭时触发的事件，作为最后一个事件
/// </summary>
public record ShutdownEvent: EventBase
{
    /// <summary>
    /// 是否是自身关闭而产生的事件, 当为 true 时是服务器关闭
    /// </summary>
    public bool ServerRequesting { get; set; }
}