namespace Onebot.Protocol.Models.Receipts;

/// <summary>
///     获取群信息
/// </summary>
public record GetGroupInfoReceipt : ReceiptBase
{
    /// <summary>
    ///     群 ID
    /// </summary>
    public string GroupId { get; set; }

    /// <summary>
    ///     群名称
    /// </summary>
    public string GroupName { get; set; }
}