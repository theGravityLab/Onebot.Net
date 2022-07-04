namespace Onebot.Protocol.Models.Receipts;

/// <summary>
///     获取群成员信息
/// </summary>
public record GetGroupMemberInfoReceipt : ReceiptBase
{
    /// <summary>
    ///     群 ID
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    ///     用户 ID
    /// </summary>
    public string Nickname { get; set; }
}