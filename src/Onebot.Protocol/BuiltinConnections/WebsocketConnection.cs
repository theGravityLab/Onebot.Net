using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Onebot.Protocol.Communications.Serialization;
using Onebot.Protocol.Models;
using Onebot.Protocol.Models.Actions;
using Onebot.Protocol.Models.Events;
using Onebot.Protocol.Models.Receipts;
using Websocket.Client;

namespace Onebot.Protocol.BuiltinConnections
{
    internal class WebsocketConnection : IConnection
    {
        private readonly string _host;
        private readonly int _port;
        private readonly string _accessToken;

        readonly WebsocketClient client;
        readonly Queue<EventBase> events = new();
        readonly Dictionary<string, IReceipt> receipts = new();

        public WebsocketConnection(string host, int port, string accessToken)
        {
            _host = host;
            _port = port;
            _accessToken = accessToken;
            

            client = new WebsocketClient(new Uri($"ws://{host}:{port}"), () =>
            {
                var x = new ClientWebSocket();
                x.Options.SetRequestHeader("Authorization", $"Bearer {accessToken}");
                return x;
            });

            client.MessageReceived.Subscribe(HandleMessage);
        }

        private void HandleMessage(ResponseMessage message)
        {
            switch (message.MessageType)
            {
                case WebSocketMessageType.Close:
                {
                    // 远端服务器关闭
                    break;
                }
                case WebSocketMessageType.Binary:
                {
                    // 暂不支持
                    break;
                }
                case WebSocketMessageType.Text:
                {
                    JObject json = JsonConvert.DeserializeObject<JObject>(message.Text);
                    if (json.ContainsKey("echo"))
                    {
                        // 回执
                    }
                    else
                    {
                        // 事件
                        var evt = ModelFactory.ConstructEvent(json);
                        events.Enqueue(evt);
                    }

                    break;
                }
            }
        }
        

        public async Task<EventBase> FetchAsync(CancellationToken token)
        {
            return await Task.Run(() =>
            {
                if (!client.IsStarted)
                {
                    client.Start().Wait(token);
                }
                while (!token.IsCancellationRequested)
                {
                    if (events.Count > 0)
                    {
                        return events.Dequeue();
                    }
                    Thread.Sleep(300);
                }

                return new ShutdownEvent();
            }, token);
        }

        public Task<IReceipt> SendAsync(IAction action)
        {
            throw new NotImplementedException();
        }

        ~WebsocketConnection()
        {
            if (client.IsRunning)
            {
                client.Stop(WebSocketCloseStatus.NormalClosure, "Normal closure");
            }
        }
    }
}