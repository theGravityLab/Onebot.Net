namespace Onebot.Protocol.Models.Events.Message;

/// <summary>
///     群消息
/// </summary>
public record GroupMessageEvent : EventBase
{
    /// <summary>
    ///     消息唯一 ID
    /// </summary>
    public string MessageId { get; set; }

    /// <summary>
    ///     消息内容
    /// </summary>
    public Messages.Message Message { get; set; }

    /// <summary>
    ///     消息内容的替代表示, 可以为空
    /// </summary>
    public string AltMessage { get; set; }

    /// <summary>
    ///     群 ID
    /// </summary>
    public string GroupId { get; set; }

    /// <summary>
    ///     用户 ID
    /// </summary>
    public string UserId { get; set; }
}