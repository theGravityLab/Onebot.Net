using System;
using Onebot.Protocol.Models.Receipts;

namespace Onebot.Protocol.Models.Actions;

/// <summary>
///     获取群信息
/// </summary>
public record GetGroupInfoAction : ActionBase
{
    protected override string Action => "get_group_info";
    protected override Type Receipt => typeof(GetGroupInfoReceipt);

    /// <summary>
    ///     群 ID
    /// </summary>
    public string GroupId { get; set; }
}