namespace Onebot.Protocol.Models.Receipts;

/// <summary>
/// 获取运行状态
/// </summary>
public record GetStatusReceipt : ReceiptBase
{
    /// <summary>
    /// 是否各项状态都符合预期，OneBot 实现各模块均正常
    /// </summary>
    public bool Good { get; set; }
    /// <summary>
    /// OneBot 实现对接的平台连接是否顺畅（如 QQ 平台为是否在线），是 good 的必要条件之一
    /// </summary>
    public bool Online { get; set; }
}