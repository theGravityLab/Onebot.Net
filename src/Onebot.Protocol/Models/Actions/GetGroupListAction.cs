using System;
using Onebot.Protocol.Models.Receipts;

namespace Onebot.Protocol.Models.Actions;

/// <summary>
///     获取群列表
/// </summary>
public record GetGroupListAction : ActionBase
{
    internal override string Action => "get_group_list";
    internal override Type Receipt => typeof(GetGroupListReceipt);
}