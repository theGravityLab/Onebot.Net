namespace Onebot.Protocol.Models.Receipts;

/// <summary>
///     获取版本信息
/// </summary>
public record GetVersionReceipt : ReceiptBase
{
    /// <summary>
    ///     OneBot 实现名称，格式 [_a-z]+
    /// </summary>
    public string Impl { get; set; }

    /// <summary>
    ///     OneBot 实现平台名称，格式 [_a-z]+
    /// </summary>
    public string Platform { get; set; }

    /// <summary>
    ///     OneBot 实现的版本号
    /// </summary>
    public string Version { get; set; }

    /// <summary>
    ///     OneBot 实现的 OneBot 标准版本号
    /// </summary>
    public string OnebotVersion { get; set; }
}