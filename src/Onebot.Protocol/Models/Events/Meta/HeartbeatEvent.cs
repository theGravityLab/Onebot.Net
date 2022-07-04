namespace Onebot.Protocol.Models.Events.Meta;

/// <summary>
///     心跳
/// </summary>
public record HeartbeatEvent : EventBase
{
    /// <summary>
    ///     到下次心跳的间隔，单位：毫秒
    /// </summary>
    public double Interval { get; set; }

    /// <summary>
    ///     OneBot 状态，与 get_status 动作响应数据一致
    /// </summary>
    public Status Status { get; set; }
}