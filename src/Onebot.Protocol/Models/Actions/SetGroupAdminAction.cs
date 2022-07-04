using System;
using Onebot.Protocol.Models.Receipts;

namespace Onebot.Protocol.Models.Actions;

/// <summary>
///     设置群管理员
/// </summary>
public record SetGroupAdminAction : ActionBase
{
    internal override string Action => "set_group_admin";
    internal override Type Receipt => typeof(SetGroupAdminReceipt);

    /// <summary>
    ///     群 ID
    /// </summary>
    public string GroupId { get; set; }

    /// <summary>
    ///     用户 ID
    /// </summary>
    public string UserId { get; set; }
}