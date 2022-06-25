namespace Onebot.Protocol.Models.Events.Notice;

/// <summary>
/// 本事件应在群消息被撤回或被管理员删除时触发。
/// </summary>
public record GroupMessageDeleteEvent : EventBase
{
    /// <summary>
    /// 群 ID
    /// </summary>
    public string GroupId { get; set; }
    /// <summary>
    /// 消息 ID
    /// </summary>
    public string MessageId { get; set; }
    /// <summary>
    /// 消息发送者 ID
    /// </summary>
    public string UserId { get; set; }
    /// <summary>
    /// 操作者 ID
    /// </summary>
    public string OperatorId { get; set; }
}