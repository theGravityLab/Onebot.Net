using System;
using Onebot.Protocol.Models.Receipts;

namespace Onebot.Protocol.Models.Actions;

/// <summary>
///     获取运行状态
/// </summary>
public record GetStatusAction : ActionBase
{
    protected override string Action => "get_status";
    protected override Type Receipt => typeof(GetStatusReceipt);
}