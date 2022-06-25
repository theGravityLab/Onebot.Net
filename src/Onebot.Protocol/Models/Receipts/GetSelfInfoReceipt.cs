namespace Onebot.Protocol.Models.Receipts;

/// <summary>
/// 获取机器人自身信息
/// </summary>
public record GetSelfInfoReceipt : ReceiptBase
{
    /// <summary>
    /// 机器人用户 ID
    /// </summary>
    public string UserId { get; set; }
    /// <summary>
    /// 机器人名称/昵称
    /// </summary>
    public string Nickname { get; set; }
}