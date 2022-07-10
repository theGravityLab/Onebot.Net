using System;
using Onebot.Protocol.Models.Receipts;

namespace Onebot.Protocol.Models.Actions;

/// <summary>
///     获取用户信息
/// </summary>
public record GetUserInfoAction : ActionBase
{
    protected override string Action => "get_user_info";
    protected override Type Receipt => typeof(GetUserInfoReceipt);

    /// <summary>
    ///     用户 ID，可以是好友，也可以是陌生人
    /// </summary>
    public string UserId { get; set; }
}