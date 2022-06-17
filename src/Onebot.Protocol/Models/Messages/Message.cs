using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Onebot.Protocol.Models.Messages;

public class Message : Collection<MessageSegment>
{
    public Message() : base()
    {
    }

    public Message(IList<MessageSegment> list) : base(list)
    {
    }
}