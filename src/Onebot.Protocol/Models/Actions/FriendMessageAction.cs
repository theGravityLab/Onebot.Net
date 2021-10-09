using System;
using System.Collections.Generic;
using Onebot.Protocol.Models.Messages;
using Onebot.Protocol.Models.Receipts;

namespace Onebot.Protocol.Models.Actions
{
    public record FriendMessageAction(long UserId, IEnumerable<MessageCell> Message) : IAction
    {
        public Type Output => typeof(MessageReceipt);
    }
}