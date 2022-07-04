using System;
using Onebot.Protocol.Models.Receipts;

namespace Onebot.Protocol.Models.Actions;

/// <summary>
///     退出群
/// </summary>
public record LeaveGroupAction : ActionBase
{
    internal override string Action => "leave_group";
    internal override Type Receipt => typeof(LeaveGroupReceipt);

    /// <summary>
    ///     群 ID
    /// </summary>
    public string GroupId { get; set; }
}