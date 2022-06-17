using System.Threading;
using System.Threading.Tasks;
using Onebot.Protocol.Models.Actions;
using Onebot.Protocol.Models.Events;
using Onebot.Protocol.Models.Receipts;

namespace Onebot.Protocol
{
    public interface IConnection
    {
        Task<ReceiptBase> SendAsync(ActionBase action, CancellationToken token);
        Task<EventBase> FetchAsync(CancellationToken token);
    }
}