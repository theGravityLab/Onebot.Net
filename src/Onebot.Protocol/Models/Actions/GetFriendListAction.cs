using System;
using Onebot.Protocol.Models.Receipts;

namespace Onebot.Protocol.Models.Actions;

/// <summary>
///     获取好友列表
/// </summary>
public record GetFriendListAction : ActionBase
{
    internal override string Action => "get_friend_list";
    internal override Type Receipt => typeof(GetFriendListReceipt);
}