using System;

namespace Onebot.Protocol.Models.Actions
{
    public record QueryMemberAction(long GroupId, long UserId): IAction
    {
        public Type Output => typeof(QueryMemberReceipt);
    }
}