using System;
using Onebot.Protocol.Models.Receipts;

namespace Onebot.Protocol.Models.Actions;

/// <summary>
/// 获取版本信息
/// </summary>
public record GetVersionAction : ActionBase
{
    internal override string Action => "get_version";
    internal override Type Receipt => typeof(GetVersionReceipt);
}