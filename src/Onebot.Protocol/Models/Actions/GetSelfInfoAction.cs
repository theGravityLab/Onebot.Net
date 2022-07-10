using System;
using Onebot.Protocol.Models.Receipts;

namespace Onebot.Protocol.Models.Actions;

/// <summary>
///     获取机器人自身信息
/// </summary>
public record GetSelfInfoAction : ActionBase
{
    protected override string Action => "get_self_info";
    protected override Type Receipt => typeof(GetSelfInfoReceipt);
}