using System.Collections;
using System.Collections.Generic;

namespace Onebot.Protocol.Models.Messages
{
    /// <summary>
    /// 消息数据类型是在 OneBot 标准中表示聊天消息内容的数据类型，标准中使用 message 表示该类型。
    /// 在 OneBot 实现传给应用端的事件字段和动作响应数据中，消息数据类型必须是消息段数组/列表。
    /// 在由应用端传给 OneBot 实现的动作请求参数中，消息数据类型建议是消息段数组/列表，也可以是单个消息段或字符串。对于字符串，OneBot 实现应将其理解为单个纯文本消息段组成的消息。
    /// </summary>
    public record MessageSegment
    {
        /// <summary>
        /// 消息段名称
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 消息段参数
        /// </summary>
        public IDictionary<string, string> Data { get; set; }

        public static MessageSegment From(string typeName, IEnumerable<KeyValuePair<string, string>> values) => new()
        {
            Type = typeName,
            Data = new Dictionary<string, string>(values)
        };
    }
}