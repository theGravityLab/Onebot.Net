namespace Onebot.Protocol.Models.Receipts;

/// <summary>
///     发送消息
/// </summary>
public record SendMessageReceipt : ReceiptBase
{
    /// <summary>
    ///     消息 ID
    /// </summary>
    public string MessageId { get; set; }

    /// <summary>
    ///     消息成功发出的时间（Unix 时间戳），单位：秒
    /// </summary>
    public long Time { get; set; }
}