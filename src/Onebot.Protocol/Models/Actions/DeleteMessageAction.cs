using System;
using Onebot.Protocol.Models.Receipts;

namespace Onebot.Protocol.Models.Actions;

/// <summary>
///     delete_message 撤回消息
/// </summary>
public record DeleteMessageAction : ActionBase
{
    protected override string Action => "delete_message";
    protected override Type Receipt => typeof(DeleteMessageReceipt);

    /// <summary>
    ///     唯一的消息 ID
    /// </summary>
    public string MessageId { get; set; }
}