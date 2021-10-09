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

var await socket.WriteAsync(new FriendMessageEvent(10001, new List<MessageCell>(){ Type = "plain", Data = new Dictionary<string, object>(){ {"text", "txsb"} } }));

// 这么写是有问题的，WriteAsync会等待ReadAsync的回执，而ReadAsync被上面的await卡死了，造成了死锁（但是我搞了超时，如果调用有了结果，并不是死锁解开了，而是超时了）。
// 要么不等待WriteAsync，要么双线程
// 当然最好的方法是换一个结构，不把回执放在ReadAsync里。

```

### Ac682 Sucks

上面的注释看了没，意思是说当前的回执模式设计会导致死锁，请用双线程模式分开对 OnebotWebsocket 进行读写。

**这真是个大 bad design**