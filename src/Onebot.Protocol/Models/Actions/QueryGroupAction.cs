using System;
using Onebot.Protocol.Models.Receipts;

namespace Onebot.Protocol.Models.Actions
{
    public record QueryGroupAction(long GroupId) : IAction
    {
        public Type Output => typeof(QueryGroupReceipt);
    }
}