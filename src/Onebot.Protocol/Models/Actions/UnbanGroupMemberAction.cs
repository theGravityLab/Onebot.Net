using System;
using Onebot.Protocol.Models.Receipts;

namespace Onebot.Protocol.Models.Actions;

/// <summary>
///     取消禁言群成员
/// </summary>
public record UnbanGroupMemberAction : ActionBase
{
    protected override string Action => "unban_group_member";
    protected override Type Receipt => typeof(UnbanGroupMemberReceipt);

    /// <summary>
    ///     群 ID
    /// </summary>
    public string GroupId { get; set; }

    /// <summary>
    ///     用户 ID
    /// </summary>
    public string UserId { get; set; }
}