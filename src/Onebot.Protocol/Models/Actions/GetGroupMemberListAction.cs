using System;
using Onebot.Protocol.Models.Receipts;

namespace Onebot.Protocol.Models.Actions;

/// <summary>
///     获取群成员列表
/// </summary>
public record GetGroupMemberListAction : ActionBase
{
    internal override string Action => "get_group_member_list";
    internal override Type Receipt => typeof(GetGroupMemberListReceipt);

    /// <summary>
    ///     群 ID
    /// </summary>
    public string GroupId { get; set; }
}