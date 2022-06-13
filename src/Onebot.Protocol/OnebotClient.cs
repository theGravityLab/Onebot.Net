using System.Collections.Generic;
using System.Threading.Tasks;
using Onebot.Protocol.Models.Actions;
using Onebot.Protocol.Models.Events.Message;
using Onebot.Protocol.Models.Messages;
using Onebot.Protocol.Models.Receipts;

namespace Onebot.Protocol
{
    public class OnebotClient
    {
        public IConnection Connection { get; private set; }

        public OnebotClient(IConnection connection)
        {
            Connection = connection;
        }

        public async Task<string> SendFriendMessageAsync(long id, IEnumerable<MessageSegment> message)
        {
            var args = new FriendMessageAction(id, message);
            var receipt = await Connection.SendAsync(args) as MessageReceipt;
            return receipt?.MessageId;
        }
    }
}