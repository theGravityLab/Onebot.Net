namespace Onebot.Protocol.Models.Events.Notice;

/// <summary>
///     本事件应在好友或关注者增加时触发。
/// </summary>
public record FriendIncreaseEvent : EventBase
{
    /// <summary>
    ///     用户 ID
    /// </summary>
    public string UserId { get; set; }
}