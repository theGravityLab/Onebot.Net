using System.Collections.Generic;
using Onebot.Protocol.Models.Messages;

namespace Onebot.Protocol.Models.Actions
{
    public record FriendMessageAction(long UserId, IEnumerable<MessageSegment> Message) : IAction;
}