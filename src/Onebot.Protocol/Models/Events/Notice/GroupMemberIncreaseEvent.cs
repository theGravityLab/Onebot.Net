namespace Onebot.Protocol.Models.Events.Notice;

/// <summary>
///     本事件应在群成员（包括机器人自身）申请加群通过、被邀请进群或其它方式进群时触发。
/// </summary>
public record GroupMemberIncreaseEvent : EventBase
{
    /// <summary>
    ///     群 ID
    /// </summary>
    public string GroupId { get; set; }

    /// <summary>
    ///     用户 ID
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    ///     操作者 ID
    /// </summary>
    public string OperatorId { get; set; }
}