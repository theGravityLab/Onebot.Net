using System;
using Newtonsoft.Json;
using Onebot.Protocol.Models.Receipts;

namespace Onebot.Protocol.Models.Actions;

/// <summary>
/// 获取群信息
/// </summary>
public record GetGroupInfoAction: ActionBase
{
    internal override string Action => "get_group_info";
    internal override Type Receipt => typeof(GetGroupInfoReceipt);

    /// <summary>
    /// 群 ID
    /// </summary>
    public string GroupId { get; set; }
}