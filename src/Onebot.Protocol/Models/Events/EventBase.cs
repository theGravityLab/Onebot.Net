namespace Onebot.Protocol.Models.Events;

public abstract record EventBase
{
    /// <summary>
    /// 事件唯一标识符
    /// </summary>
    public string Id { get; set; }
    /// <summary>
    /// OneBot 实现名称，格式 [_a-z]+
    /// </summary>
    public string Impl { get; set; }
    /// <summary>
    /// OneBot 实现平台名称，格式 [_a-z]+
    /// </summary>
    public string Platform { get; set; }
    /// <summary>
    /// 机器人自身 ID
    /// </summary>
    public string SelfId { get; set; }
    /// <summary>
    /// 事件发生时间（Unix 时间戳），单位：秒
    /// </summary>
    public double Time { get; set; }
    /// <summary>
    /// 事件类型，必须是 meta、message、notice、request 中的一个，分别表示元事件、消息事件、通知事件和请求事件
    /// </summary>
    public string Type { get; set; }
    /// <summary>
    /// 事件详细类型
    /// </summary>
    public string DetailType { get; set; }
    /// <summary>
    /// 事件子类型（详细类型的下一级类型）
    /// </summary>
    public string SubType { get; set; }
}