using System;
using Onebot.Protocol.Models.Receipts;

namespace Onebot.Protocol.Models.Actions;

/// <summary>
///     获取运行状态
/// </summary>
public record GetStatusAction : ActionBase
{
    internal override string Action => "get_status";
    internal override Type Receipt => typeof(GetStatusReceipt);
}