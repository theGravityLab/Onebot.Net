using System;
using Onebot.Protocol.Models.Receipts;

namespace Onebot.Protocol.Models.Actions;

/// <summary>
///     踢出群成员
/// </summary>
public record KickGroupMemberAction : ActionBase
{
    protected override string Action => "kick_group_member";
    protected override Type Receipt => typeof(KickGroupMemberReceipt);

    /// <summary>
    ///     群 ID
    /// </summary>
    public string GroupId { get; set; }

    /// <summary>
    ///     用户 ID
    /// </summary>
    public string UserId { get; set; }
}