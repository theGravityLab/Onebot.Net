using System.Collections.Generic;
using Onebot.Protocol.Models.Messages;
using Onebot.Protocol.Models.Relations;

namespace Onebot.Protocol.Models.Events
{
    public record GroupMessageEvent(long GroupId, long UserId, IEnumerable<MessageCell> Message, long MessageId, int Font,
        Sender Sender) : IEvent;
}