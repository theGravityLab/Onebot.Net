using System.Threading.Tasks;
using Onebot.Protocol.Models.Actions;
using Onebot.Protocol.Models.Events;
using Onebot.Protocol.Models.Receipts;

namespace Onebot.Protocol.BuiltinConnections
{
    internal  class WebsocketConnection : IConnection
    {
        public Task<bool> ConnectAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEvent> FetchAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<IReceipt> SendAsync(IAction action)
        {
            throw new System.NotImplementedException();
        }
    }
}