using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Text.Json;
using System.Threading.Tasks;
using Onebot.Protocol.Communications.Serialization;
using Onebot.Protocol.Communications.Serialization.Converters;
using Onebot.Protocol.Extensions;
using Onebot.Protocol.Models.Actions;
using Onebot.Protocol.Models.Events;
using Onebot.Protocol.Models.Messages;
using Onebot.Protocol.Models.Receipts;
using Onebot.Protocol.Models.Relations;

namespace Onebot.Protocol.Communications
{
    public class OnebotWebsocket
    {
        private readonly ClientWebSocket _socket;
        private readonly JsonSerializerOptions _options;

        private readonly Dictionary<string, object> awaitings = new();

        public OnebotWebsocket(ClientWebSocket socket)
        {
            _socket = socket;
            _options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance,
                Converters = { new GenderConverter(), new RoleConverter() },
                IgnoreReadOnlyProperties = true
            };
        }

        public static OnebotWebsocket Connect(string host, int port, string accessToken)
        {
            var url = $"ws://{host}:{port}/?access_token={accessToken}";
            var socket = new ClientWebSocket();
            socket.ConnectAsync(new Uri(url, UriKind.Absolute), CancellationToken.None).Wait();
            var instance = new OnebotWebsocket(socket);
            return instance;
        }

        public async Task<IReceipt> WriteAsync(IAction args)
        {
            var wrapper = MakeAction(args);
            var json = JsonSerializer.Serialize(wrapper, _options);
            var data = Encoding.UTF8.GetBytes(json);
            var buffer = new ArraySegment<byte>(data);
            await _socket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
            awaitings.Add(wrapper.Echo, args.Output);
            int esp = 0;
            while (true)
            {
                var obj = awaitings[wrapper.Echo];
                if (obj is IReceipt receipt)
                {
                    awaitings.Remove(wrapper.Echo);
                    return receipt;
                }
                else
                {
                    await Task.Delay(300);
                    esp += 300;
                }

                if (esp >= 3000)
                {
                    return default;
                }
            }
        }

        public async Task<IEvent> ReadAsync(CancellationToken token)
        {
            var bytes = new byte[1024];
            var buffer = new ArraySegment<byte>(bytes);
            var result = await _socket.ReceiveAsync(buffer, token);
            var data = buffer.Skip(buffer.Offset).Take(result.Count).ToArray();
            var msg = Encoding.UTF8.GetString(data);
            var evt = ParseEvent(msg);
            if (evt == null)
            {
                return await ReadAsync(token);
            }

            return evt;
        }

        private ActionWrapper MakeAction(IAction action) => new()
        {
            Action = action switch
            {
                FriendMessageAction friend => "send_private_msg",
                GroupMessageAction group => "send_group_msg"
            },
            Params = action
        };

        private IEvent ParseEvent(string json)
        {
            var obj = JsonSerializer.Deserialize<JsonElement>(json);
            if (obj.TryGetProperty("post_type", out var post_type))
            {
                switch (post_type.GetString())
                {
                    case "meta_event":
                    {
                        var meta_event = obj.GetProperty("meta_event_type");
                        switch (meta_event.GetString())
                        {
                            case "lifecycle":
                                var sub_type = obj.GetProperty("sub_type");
                                switch (sub_type.GetString())
                                {
                                    case "connect":
                                        return new ConnectEvent();
                                }

                                break;
                            case "heartbeat":
                                return new HeartbeatEvent();
                        }

                        break;
                    }
                    case "message":
                    {
                        var message_type = obj.GetProperty("message_type");
                        switch (message_type.GetString())
                        {
                            case "group":

                            {
                                var sub_type = obj.GetProperty("sub_type");
                                switch (sub_type.GetString())
                                {
                                    case "normal":
                                        return JsonSerializer.Deserialize<GroupMessageEvent>(json, _options);
                                }

                                break;
                            }
                            case "private":
                            {
                                var sub_type = obj.GetProperty("sub_type");
                                switch (sub_type.GetString())
                                {
                                    case "friend":
                                        return JsonSerializer.Deserialize<FriendMessageEvent>(json, _options);
                                    case "group":
                                        // temp conversation
                                        break;
                                    case "group_self":
                                        // group self-send
                                        break;
                                    case "other":
                                        // other
                                        break;
                                }

                                break;
                            }
                        }

                        break;
                    }
                }

                return new UnknownEvent(json);
            }

            if (obj.TryGetProperty("echo", out var echo) && awaitings.ContainsKey(echo.GetString()) && awaitings[echo.GetString()] is Type)
            {
                var receiptType = (Type)awaitings[echo.GetString()];
                var type = typeof(ReceiptWrapper<>).MakeGenericType(receiptType);
                var receipt =  (ReceiptWrapper<IReceipt>)JsonSerializer.Deserialize(json, type, _options);
                awaitings[echo.GetString()] = receipt.Data;
            }


            return null;
        }
    }
}