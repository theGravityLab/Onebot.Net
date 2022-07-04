using System;
using Onebot.Protocol.Models.Receipts;

namespace Onebot.Protocol.Models.Actions;

/// <summary>
///     设置群名称
/// </summary>
public record SetGroupNameAction : ActionBase
{
    internal override string Action => "set_group_name";
    internal override Type Receipt => typeof(SetGroupNameReceipt);

    /// <summary>
    ///     群 ID
    /// </summary>
    public string GroupId { get; set; }

    /// <summary>
    ///     新群名称
    /// </summary>
    public string GroupName { get; set; }
}