using System;
using Onebot.Protocol.Models.Receipts;

namespace Onebot.Protocol.Models.Actions;

/// <summary>
///     退出群
/// </summary>
public record LeaveGroupAction : ActionBase
{
    protected override string Action => "leave_group";
    protected override Type Receipt => typeof(LeaveGroupReceipt);

    /// <summary>
    ///     群 ID
    /// </summary>
    public string GroupId { get; set; }
}