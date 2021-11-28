using System;
using Onebot.Protocol.Models.Receipts;

namespace Onebot.Protocol.Models.Actions
{
    public record QueryMemberAction(long GroupId, long UserId): IAction
    {
        public Type Output => typeof(QueryMemberReceipt);
    }
}