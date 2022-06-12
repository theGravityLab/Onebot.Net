using System.Collections.Generic;
using System.Threading.Tasks;
using Onebot.Protocol.Models.Actions;
using Onebot.Protocol.Models.Messages;
using Onebot.Protocol.Models.Receipts;

namespace Onebot.Protocol
{
    public class OnebotClient
    {
        readonly IConnection _connection;

        public OnebotClient(IConnection connection)
        {
            _connection = connection;
        }

        public async Task<string> SendFriendMessageAsync(long id, IEnumerable<MessageCell> message)
        {
            var args = new FriendMessageAction(id, message);
            var receipt = await _connection.SendAsync(args) as MessageReceipt;

            return receipt.MessageId;
        }
    }
}