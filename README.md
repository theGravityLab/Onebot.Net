# Onebot.Net
<p align="center">
    <a href="https://onebot.dev/ecosystem.html" alt="onebot">
        <img src="https://img.shields.io/badge/OneBot-12-black?logo=data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAHAAAABwCAMAAADxPgR5AAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAAxQTFRF////29vbr6+vAAAAk1hCcwAAAAR0Uk5T////AEAqqfQAAAKcSURBVHja7NrbctswDATQXfD//zlpO7FlmwAWIOnOtNaTM5JwDMa8E+PNFz7g3waJ24fviyDPgfhz8fHP39cBcBL9KoJbQUxjA2iYqHL3FAnvzhL4GtVNUcoSZe6eSHizBcK5LL7dBr2AUZlev1ARRHCljzRALIEog6H3U6bCIyqIZdAT0eBuJYaGiJaHSjmkYIZd+qSGWAQnIaz2OArVnX6vrItQvbhZJtVGB5qX9wKqCMkb9W7aexfCO/rwQRBzsDIsYx4AOz0nhAtWu7bqkEQBO0Pr+Ftjt5fFCUEbm0Sbgdu8WSgJ5NgH2iu46R/o1UcBXJsFusWF/QUaz3RwJMEgngfaGGdSxJkE/Yg4lOBryBiMwvAhZrVMUUvwqU7F05b5WLaUIN4M4hRocQQRnEedgsn7TZB3UCpRrIJwQfqvGwsg18EnI2uSVNC8t+0QmMXogvbPg/xk+Mnw/6kW/rraUlvqgmFreAA09xW5t0AFlHrQZ3CsgvZm0FbHNKyBmheBKIF2cCA8A600aHPmFtRB1XvMsJAiza7LpPog0UJwccKdzw8rdf8MyN2ePYF896LC5hTzdZqxb6VNXInaupARLDNBWgI8spq4T0Qb5H4vWfPmHo8OyB1ito+AysNNz0oglj1U955sjUN9d41LnrX2D/u7eRwxyOaOpfyevCWbTgDEoilsOnu7zsKhjRCsnD/QzhdkYLBLXjiK4f3UWmcx2M7PO21CKVTH84638NTplt6JIQH0ZwCNuiWAfvuLhdrcOYPVO9eW3A67l7hZtgaY9GZo9AFc6cryjoeFBIWeU+npnk/nLE0OxCHL1eQsc1IciehjpJv5mqCsjeopaH6r15/MrxNnVhu7tmcslay2gO2Z1QfcfX0JMACG41/u0RrI9QAAAABJRU5ErkJggg==" /></a>
    <a href="https://www.nuget.org/packages/Onebot.Protocol" alt="nuget">
        <img src="https://img.shields.io/nuget/v/Onebot.Protocol" /></a>
    <a href="https://dotnet.microsoft.com/" alt="dotnet">
        <img src="https://img.shields.io/badge/dotnet-6.0-blueviolet"/></a>
    <a href="https://github.com/theGravityLab/Onebot.Net" alt="licecnse">
        <img src="https://img.shields.io/github/license/theGravityLab/Onebot.Net" /></a>
</p>

Onebot 的 .Net SDK。

## 安装

```sh
dotnet add package Onebot.Protocol
```

~~虽然叫 Protocol 其实多种 `IConnection` 实现都被包含在这个包，懒得搞 Onebot.Websocket Onebot.Http 之类的包了~~

## 示例

### 基本使用

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
void Configure(IServiceCollection services) => services
    .AddSingleton<IConnection>(() => ConnectionFactory.FromWebsocket(...))
    .AddTransient<OnebotClient>();

class Foo
{
    readonly OnebotClient _client;
    public Foo(OnebotClient client) => _client = client;
    void Bar() => _client.SendPrivateMessageAsync(...);
}
```

# 文档

就一个 `OnebotClient` 和 `IConnection`，其他模型全部按照 Onebot 接口定义。
`IConnection` 负责将事件和动作与服务器传递并取得回执。`OnebotClient` 将事件封装为方法，更便于调用。

接口定义请参阅 [Onebot](https://12.onebot.dev/interface) 的对应部分。

# 实现

## 元接口

### 元事件

- [x] meta.heartbeat 心跳
### 元动作
- [ ] get_latest_events 获取最新事件列表
- [ ] get_supported_actions 获取支持的动作列表
- [ ] get_status 获取运行状态
- [ ] get_version 获取版本信息

## 消息接口

### 消息段

- [x] 基本消息创建
- [ ] ~~基本消息解析(大概率不会支持)~~
- [x] 扩展

### 消息动作

- [x] send_message 发送消息
- [x] delete_message 撤回消息

## 单用户接口

### 用户消息事件

- [x] message.private 私聊消息

### 用户通知事件

- [x] notice.friend_increase 好友增加
- [x] notice.friend_decrease 好友减少
- [x] notice.private_message_delete 私聊消息删除

### 用户动作

- [x] get_self_info 获取机器人自身信息
- [x] get_user_info 获取用户信息
- [x] get_friend_list 获取好友列表

## 单级群组接口

### 群消息事件

- [x] message.group 群消息

### 群通知事件

- [x] notice.group_member_increase 群成员增加
- [x] notice.group_member_decrease 群成员减少
- [x] notice.group_member_ban 群成员禁言
- [x] notice.group_member_unban 群成员解除禁言
- [x] notice.group_admin_set 群管理员设置
- [x] notice.group_admin_unset 群管理员取消设置
- [x] notice.group_message_delete 群消息删除

### 群动作

- [x] get_group_info 获取群信息
- [x] get_group_list 获取群列表
- [x] get_group_member_info 获取群成员信息
- [x] get_group_member_list 获取群成员列表
- [x] set_group_name 设置群名称
- [x] leave_group 退出群
- [x] kick_group_member 踢出群成员
- [x] ban_group_member 禁言群成员
- [x] unban_group_member 取消禁言群成员
- [x] set_group_admin 设置群管理员
- [x] unset_group_admin 取消设置群管理员

## 两级群组接口

### 群组消息事件

- [ ] message.channel 频道消息

### 群组通知事件

- [ ] notice.guild_member_increase 群组成员增加
- [ ] notice.guild_member_decrease 群组成员减少
- [ ] notice.channel_message_delete 频道消息删除
- [ ] notice.channel_create 频道新建
- [ ] notice.channel_delete 频道删除

### 群组动作

- [ ] get_guild_info 获取群组信息
- [ ] get_guild_list 获取群组列表
- [ ] get_channel_info 获取频道信息
- [ ] get_channel_list 获取频道列表
- [ ] get_guild_member_info 获取群组成员信息
- [ ] get_guild_member_list 获取群组成员列表
- [ ] set_guild_name 设置群组名称
- [ ] set_channel_name 设置频道名称
- [ ] leave_guild 退出群组

## 文件接口

### 文件动作

- [x] upload_file 上传文件
- [ ] upload_file_fragmented 分片上传文件
- [x] get_file 获取文件
- [ ] get_file_fragmented 分片获取文件
