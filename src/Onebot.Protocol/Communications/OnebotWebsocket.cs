using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Onebot.Protocol.Communications.Serialization;
using Onebot.Protocol.Communications.Serialization.Converters;
using Onebot.Protocol.Extensions;
using Onebot.Protocol.Models.Actions;
using Onebot.Protocol.Models.Events;
using Onebot.Protocol.Models.Messages;
using Onebot.Protocol.Models.Receipts;
using Onebot.Protocol.Models.Relations;
using Websocket.Client;

namespace Onebot.Protocol.Communications
{
    public class OnebotWebsocket
    {
        private readonly WebsocketClient _client;
        private readonly JsonSerializerOptions _options;


        private readonly Queue<string> receiptQueue = new();
        private readonly Queue<JsonElement> eventQueue = new();

        public OnebotWebsocket(WebsocketClient client)
        {
            _client = client;
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
            var url = $"ws://{host}:{port}";
            var client = new WebsocketClient(new Uri(url, UriKind.Absolute), () =>
            {
                var socket = new ClientWebSocket();
                socket.Options.SetRequestHeader("Authorization", $"Bearer {accessToken}");
                return socket;
            });
            var instance = new OnebotWebsocket(client);
            client.MessageReceived.Subscribe((message) =>
                instance.Get(message.Text)
            );
            client.Start().Wait();
            return instance;
        }

        public IReceipt Write(IAction args, CancellationToken token)
        {
            var wrapper = MakeAction(args);
            var json = JsonSerializer.Serialize(wrapper, _options);
            _client.Send(json);
            while (receiptQueue.Count == 0 && !token.IsCancellationRequested)
            {
                Thread.Sleep(100);
            }
            if (token.IsCancellationRequested) return null;

            var receiptJson = receiptQueue.Dequeue();
            var receiptType = args.Output;
            var type = typeof(ReceiptWrapper<>).MakeGenericType(receiptType);
            var receipt = (ReceiptWrapper<IReceipt>)JsonSerializer.Deserialize(receiptJson, type, _options);
            return receipt.Data;
        }

        public IEvent Read(CancellationToken token)
        {
            while (eventQueue.Count == 0 && !token.IsCancellationRequested)
            {
                Thread.Sleep(100);
            }
            if (token.IsCancellationRequested) return null;

            var evt = eventQueue.Dequeue();
            return ParseEvent(evt);
        }

        private void Get(string msg)
        {
            if (string.IsNullOrWhiteSpace(msg))
            {
                return;
            }
            var obj = JsonSerializer.Deserialize<JsonElement>(msg);
            if (obj.TryGetProperty("post_type", out var _))
            {
                eventQueue.Enqueue(obj);
            }
            else if (obj.TryGetProperty("echo", out var _))
            {
                receiptQueue.Enqueue(msg);
            }
        }

        private ActionWrapper MakeAction(IAction action) => new()
        {
            Action = action switch
            {
                FriendMessageAction it => "send_private_msg",
                GroupMessageAction it => "send_group_msg",
                QueryGroupAction it => "get_group_info",
                QueryMemberAction it => "get_group_member_info"
            },
            Params = action
        };

        private IEvent ParseEvent(JsonElement obj)
        {
            var post_type = obj.GetProperty("post_type");
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
                                            return JsonSerializer.Deserialize<GroupMessageEvent>(obj.GetRawText(), _options);
                                    }

                                    break;
                                }
                            case "private":
                                {
                                    var sub_type = obj.GetProperty("sub_type");
                                    switch (sub_type.GetString())
                                    {
                                        case "friend":
                                            return JsonSerializer.Deserialize<FriendMessageEvent>(obj.GetRawText(), _options);
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

            return new UnknownEvent(obj.GetRawText());
        }
    }
}