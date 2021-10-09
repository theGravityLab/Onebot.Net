# Onebot.Net

和支持 Onebot 协议的服务器贴贴。

### Onebot Sucks

Message 触犯了多态序列化，为了优雅，当前版本不实现各种消息，请自行 bind.

## 安装

```sh
dotnet add package Onebot.Protocol
```

## 示例

```csharp
var socket = OnebotWebsocket.Connect("localhost", 7890, "CALLMEFATHER");

var receipt = await socket.WriteAsync(new FriendMessageEvent(10001, new List<MessageCell>(){ Type = "plain", Data = new Dictionary<string, object>(){ {"text", "txsb"} } }));

var evt = await socket.ReadAsync(CancellationToken.None);
Console.WriteLine(evt);

```
