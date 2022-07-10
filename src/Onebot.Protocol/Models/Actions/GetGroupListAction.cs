using System;
using Onebot.Protocol.Models.Receipts;

namespace Onebot.Protocol.Models.Actions;

/// <summary>
///     获取群列表
/// </summary>
public record GetGroupListAction : ActionBase
{
    protected override string Action => "get_group_list";
    protected override Type Receipt => typeof(GetGroupListReceipt);
}