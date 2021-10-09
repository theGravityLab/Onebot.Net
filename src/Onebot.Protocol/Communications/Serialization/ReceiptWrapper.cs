using Onebot.Protocol.Models.Receipts;

namespace Onebot.Protocol.Communications.Serialization
{
    public class ReceiptWrapper<T>
    where T:IReceipt
    {
        public string Status { get; set; }
        public int RetCode { get; set; }
        public T Data { get; set; }
        public string Echo { get; set; }
    }
}