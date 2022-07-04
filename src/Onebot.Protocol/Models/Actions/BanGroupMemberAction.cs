using System;
using Onebot.Protocol.Models.Receipts;

namespace Onebot.Protocol.Models.Actions;

/// <summary>
///     禁言群成员
/// </summary>
public record BanGroupMemberAction : ActionBase
{
    internal override string Action => "ban_group_member";
    internal override Type Receipt => typeof(BanGroupMemberReceipt);

    /// <summary>
    ///     群 ID
    /// </summary>
    public string GroupId { get; set; }

    /// <summary>
    ///     用户 ID
    /// </summary>
    public string UserId { get; set; }
}