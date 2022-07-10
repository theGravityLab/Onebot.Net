using System;
using Onebot.Protocol.Models.Receipts;

namespace Onebot.Protocol.Models.Actions;

/// <summary>
///     获取好友列表
/// </summary>
public record GetFriendListAction : ActionBase
{
    protected override string Action => "get_friend_list";
    protected override Type Receipt => typeof(GetFriendListReceipt);
}