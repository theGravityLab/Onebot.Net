namespace Onebot.Protocol.Models.Events.Message;

/// <summary>
/// 私聊消息
/// </summary>
public record PrivateMessageEvent : EventBase
{
    /// <summary>
    /// 消息唯一 ID
    /// </summary>
    public string MessageId;
    /// <summary>
    /// 消息内容
    /// </summary>
    public Messages.Message Message;
    /// <summary>
    /// 消息内容的替代表示, 可以为空
    /// </summary>
    public string AltMessage;
    /// <summary>
    /// 用户 ID
    /// </summary>
    public string UserId;
}