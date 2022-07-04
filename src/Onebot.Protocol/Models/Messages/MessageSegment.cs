using System.Collections.Generic;

namespace Onebot.Protocol.Models.Messages;

/// <summary>
///     消息数据类型是在 OneBot 标准中表示聊天消息内容的数据类型，标准中使用 message 表示该类型。
///     在 OneBot 实现传给应用端的事件字段和动作响应数据中，消息数据类型必须是消息段数组/列表。
///     在由应用端传给 OneBot 实现的动作请求参数中，消息数据类型建议是消息段数组/列表，也可以是单个消息段或字符串。对于字符串，OneBot 实现应将其理解为单个纯文本消息段组成的消息。
/// </summary>
public record MessageSegment
{
    /// <summary>
    ///     消息段名称
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    ///     消息段参数
    /// </summary>
    public IDictionary<string, object> Data { get; set; }

    public static MessageSegment From(string typeName, IEnumerable<KeyValuePair<string, object>> values)
    {
        return new MessageSegment
        {
            Type = typeName,
            Data = new Dictionary<string, object>(values)
        };
    }

    public static MessageSegment Text(string text)
    {
        return From("text", new[]
        {
            new KeyValuePair<string, object>("text", text)
        });
    }

    public static MessageSegment Mention(string userId)
    {
        return From("mention", new[]
        {
            new KeyValuePair<string, object>("mention", userId)
        });
    }

    public static MessageSegment MentionAll()
    {
        return From("mention_all", null);
    }

    public static MessageSegment Image(string fileId)
    {
        return From("image", new[]
        {
            new KeyValuePair<string, object>("file_id", fileId)
        });
    }

    public static MessageSegment Voice(string fileId)
    {
        return From("voice", new[]
        {
            new KeyValuePair<string, object>("file_id", fileId)
        });
    }

    public static MessageSegment Audio(string fileId)
    {
        return From("audio", new[]
        {
            new KeyValuePair<string, object>("file_id", fileId)
        });
    }

    public static MessageSegment Video(string fileId)
    {
        return From("video", new[]
        {
            new KeyValuePair<string, object>("file_id", fileId)
        });
    }

    public static MessageSegment File(string fileId)
    {
        return From("file", new[]
        {
            new KeyValuePair<string, object>("file_id", fileId)
        });
    }

    public static MessageSegment Location(string latitude, string longitude, string title, string content)
    {
        return From(
            "location", new[]
            {
                new KeyValuePair<string, object>("latitude", latitude),
                new KeyValuePair<string, object>("longitude", longitude),
                new KeyValuePair<string, object>("title", title),
                new KeyValuePair<string, object>("content", content)
            });
    }

    public static MessageSegment Reply(string messageId, string userId)
    {
        return From("reply", new[]
        {
            new KeyValuePair<string, object>("message_id", messageId),
            new KeyValuePair<string, object>("user_id", userId)
        });
    }
}