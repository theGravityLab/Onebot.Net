using System;
using Onebot.Protocol.Models.Receipts;

namespace Onebot.Protocol.Models.Actions;

/// <summary>
///     获取群成员列表
/// </summary>
public record GetGroupMemberListAction : ActionBase
{
    protected override string Action => "get_group_member_list";
    protected override Type Receipt => typeof(GetGroupMemberListReceipt);

    /// <summary>
    ///     群 ID
    /// </summary>
    public string GroupId { get; set; }
}