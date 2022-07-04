using System;
using Onebot.Protocol.Models.Receipts;

namespace Onebot.Protocol.Models.Actions;

/// <summary>
///     取消设置群管理员
/// </summary>
public record UnsetGroupAdminAction : ActionBase
{
    internal override string Action => "unset_group_admin";
    internal override Type Receipt => typeof(UnsetGroupAdminReceipt);

    /// <summary>
    ///     群 ID
    /// </summary>
    public string GroupId { get; set; }

    /// <summary>
    ///     用户 ID
    /// </summary>
    public string UserId { get; set; }
}