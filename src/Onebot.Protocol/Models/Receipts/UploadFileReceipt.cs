namespace Onebot.Protocol.Models.Receipts;

/// <summary>
///     上传文件
/// </summary>
public record UploadFileReceipt : ReceiptBase
{
    /// <summary>
    ///     文件 ID，可供以后使用
    /// </summary>
    public string FileId { get; set; }
}