namespace Onebot.Protocol.Models.Events.Notice;

/// <summary>
/// 本事件应在私聊消息被删除时触发。
/// </summary>
public record PrivateMessageDeleteEvent : EventBase
{
    /// <summary>
    /// 消息 ID
    /// </summary>
    public string MessageId { get; set; }
    
    /// <summary>
    /// 消息发送者 ID
    /// </summary>
    public string UserId { get; set; }
}