using System;
using Onebot.Protocol.Models.Receipts;

namespace Onebot.Protocol.Models.Actions;

/// <summary>
///     获取支持的动作列表
/// </summary>
public record GetSupportedActionsAction : ActionBase
{
    protected override string Action => "get_supported_actions";
    protected override Type Receipt => typeof(GetSupportedActionsReceipt);
}