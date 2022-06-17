using System;
using System.Collections.Generic;
using Onebot.Protocol.Models.Receipts;

namespace Onebot.Protocol.Models.Actions;

/// <summary>
/// 上传文件
/// </summary>
public record UploadFileAction : ActionBase
{
    internal override string Action => "upload_file";
    internal override Type Receipt => typeof(UploadFileReceipt);

    /// <summary>
    /// 上传文件的方式，可以为 url、path、data 或扩展的方式
    /// </summary>
    public string Type { get; set; }
    /// <summary>
    /// 文件名，如 foo.jpg
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 文件 URL，当 type 为 url 时必须传入，OneBot 实现必须支持以 HTTP(S) 协议从此 URL 下载要上传的文件
    /// </summary>
    public string Url { get; set; }
    /// <summary>
    /// 下载 URL 时需要添加的 HTTP 请求头，可选传入，当 type 为 url 时 OneBot 实现必须在请求 URL 时加上这些请求头
    /// </summary>
    public IEnumerable<KeyValuePair<string, string>> Headers { get; set; }
    /// <summary>
    /// 文件路径，当 type 为 path 时必须传入，OneBot 实现必须能从此路径访问要上传的文件
    /// </summary>
    public string Path { get; set; }
    /// <summary>
    /// 文件数据，当 type 为 data 时必须传入
    /// </summary>
    public byte[] Data { get; set; }
    /// <summary>
    /// 文件数据（原始二进制）的 SHA256 校验和，全小写，可选传入
    /// </summary>
    public string Sha256 { get; set; }
}