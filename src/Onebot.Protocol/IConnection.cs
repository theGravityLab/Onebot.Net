using System.Threading.Tasks;
using Onebot.Protocol.Models.Actions;
using Onebot.Protocol.Models.Events;
using Onebot.Protocol.Models.Receipts;

namespace Onebot.Protocol
{
    public interface IConnection
    {
        Task<IReceipt> SendAsync(IAction action);
        Task<IEvent> FetchAsync();

        Task<bool> ConnectAsync();
    }
}