using Onebot.Protocol.Models.Receipts;
using Onebot.Protocol.Models.Relations;

namespace Onebot.Protocol.Models.Receipts
{
    public record QueryMemberReceipt(long GroupId, long UserId, string Nickname, string Card, Gender Sex, int Age, string Area, bool Unfriendly, string Title): IReceipt;
}