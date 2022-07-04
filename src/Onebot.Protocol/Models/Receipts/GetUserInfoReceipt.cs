namespace Onebot.Protocol.Models.Receipts;

/// <summary>
///     获取用户信息
/// </summary>
public record GetUserInfoReceipt : ReceiptBase
{
    /// <summary>
    ///     用户 ID
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    ///     用户名称/昵称
    /// </summary>
    public string Nickname { get; set; }
}