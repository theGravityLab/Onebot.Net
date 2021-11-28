using System.Text.RegularExpressions;

namespace Onebot.Protocol.Models.Receipts
{
    public record QueryGroupReceipt(long GroupId, string GroupName, string GroupMemo, uint GroupCreateTime, uint GroupLevel, int MemberCount, int MaxMemberCount): IReceipt;
}