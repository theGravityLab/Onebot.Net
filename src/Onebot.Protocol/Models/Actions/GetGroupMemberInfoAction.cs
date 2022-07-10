using System;
using Onebot.Protocol.Models.Receipts;

namespace Onebot.Protocol.Models.Actions;

/// <summary>
///     获取群成员信息
/// </summary>
public record GetGroupMemberInfoAction : ActionBase
{
    protected override string Action => "get_group_member_info";
    protected override Type Receipt => typeof(GetGroupMemberInfoReceipt);

    /// <summary>
    ///     群 ID
    /// </summary>
    public string GroupId { get; set; }

    /// <summary>
    ///     用户 ID
    /// </summary>
    public string UserId { get; set; }
}