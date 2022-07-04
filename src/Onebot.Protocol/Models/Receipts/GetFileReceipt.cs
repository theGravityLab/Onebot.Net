using System.Collections.Generic;

namespace Onebot.Protocol.Models.Receipts;

/// <summary>
///     获取文件
/// </summary>
public record GetFileReceipt : ReceiptBase
{
    /// <summary>
    ///     文件名，如 foo.jpg
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     文件 URL，当 type 为 url 时必须返回，应用端必须能以 HTTP(S) 协议从此 URL 下载文件
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    ///     下载 URL 时需要添加的 HTTP 请求头，可选返回
    /// </summary>
    public IDictionary<string, string> Headers { get; set; }

    /// <summary>
    ///     文件路径，当 type 为 path 时必须返回，应用端必须能从此路径访问文件
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    ///     文件数据，当 type 为 data 时必须返回
    /// </summary>
    public byte[] Data { get; set; }

    /// <summary>
    ///     文件数据（原始二进制）的 SHA256 校验和，全小写，可选返回
    /// </summary>
    public string Sha256 { get; set; }
}