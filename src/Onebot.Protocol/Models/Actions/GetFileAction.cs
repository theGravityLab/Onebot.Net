using System;
using Onebot.Protocol.Models.Receipts;

namespace Onebot.Protocol.Models.Actions;

/// <summary>
///     获取文件
/// </summary>
public record GetFileAction : ActionBase
{
    internal override string Action => "get_file";
    internal override Type Receipt => typeof(GetFileReceipt);

    /// <summary>
    ///     文件 ID
    /// </summary>
    public string FileId { get; set; }

    /// <summary>
    ///     获取文件的方式，可以为 url、path、data 或扩展的方式
    /// </summary>
    public string Type { get; set; }
}