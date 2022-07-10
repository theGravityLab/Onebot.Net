using System;
using Onebot.Protocol.Models.Messages;
using Onebot.Protocol.Models.Receipts;

namespace Onebot.Protocol.Models.Actions;

/// <summary>
///     发送消息
/// </summary>
public record SendMessageAction : ActionBase
{
    protected override string Action => "send_message";
    protected override Type Receipt => typeof(SendMessageReceipt);

    /// <summary>
    ///     发送的类型，可以为 private、group、channel 或扩展的类型，和消息事件的 detail_type 字段对应
    /// </summary>
    public string DetailType { get; set; }

    /// <summary>
    ///     用户 ID，当 detail_type 为 private 时必须传入
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    ///     群 ID，当 detail_type 为 group 时必须传入
    /// </summary>
    public string GroupId { get; set; }

    /// <summary>
    ///     Guild 群组 ID，当 detail_type 为 channel 时必须传入
    /// </summary>
    public string GuildId { get; set; }

    /// <summary>
    ///     频道 ID，当 detail_type 为 channel 时必须传入
    /// </summary>
    public string ChannelId { get; set; }

    /// <summary>
    ///     消息内容
    /// </summary>
    public Message Message { get; set; }
}