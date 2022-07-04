using System;
using Onebot.Protocol.Models.Receipts;

namespace Onebot.Protocol.Models.Actions;

/// <summary>
///     获取支持的动作列表
/// </summary>
public record GetSupportedActionsAction : ActionBase
{
    internal override string Action => "get_supported_actions";
    internal override Type Receipt => typeof(GetSupportedActionsReceipt);
}