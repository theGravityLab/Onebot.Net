# Onebot.Net

和支持 Onebot V12 协议的服务器贴贴。

### Onebot Sucks

Message 触犯了多态序列化，为了优雅，当前版本不实现各种消息，请自行 bind.

直接摆烂，整个都是按照 Onebot 协议写的，字段名一个都没改。

## 安装

```sh
dotnet add package Onebot.Protocol
```

~~ 虽然叫 Protocol 其实多种 `IConnection` 实现都被包含在这个包，懒得搞 Onebot.Websocket Onebot.Http 之类的包了 ~~

## 示例

### 创建链接

```csharp
var client = new OnebotClient(ConnectionFactory.FromWebsocket("localhost", 7890, "WHO_S_YOUR_DADDY"));
await client.SendPrivateMessageAsync("10000", new Message()
{
    new MessageSegment()
    {
        Type = "text",
        Data = new Dictionary<string, string>()
        {
            {"text", "Ob 的消息模型就是这样的，是不是很原汁原味？就问你还原的像不像吧。"}
        }
    }
});

```

### 配合容器

```csharp
services
    .AddSingleton<IConnection>(() => ConnectionFactory.FromWebsocket(...))
    .AddTransient<OnebotClient>();

class Foo
{
    readonly OnebotClient _client;
    Foo(OnebotClient client) => _client = client;
    Bar() => _client.SendPrivateMessageAsync(...);
}
```

# 文档

就一个 `OnebotClient` 和 `IConnection`，其他模型全部按照 Onebot 接口定义。
`IConnection` 负责将事件和动作与服务器传递并取得回执。`OnebotClient` 将事件封装为方法，更便于调用。