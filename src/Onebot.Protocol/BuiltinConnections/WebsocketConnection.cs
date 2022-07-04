using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Onebot.Protocol.Models;
using Onebot.Protocol.Models.Actions;
using Onebot.Protocol.Models.Events;
using Onebot.Protocol.Models.Receipts;
using Websocket.Client;

namespace Onebot.Protocol.BuiltinConnections;

internal class WebsocketConnection : IConnection
{
    private readonly string _accessToken;
    private readonly string _host;
    private readonly int _port;

    private readonly WebsocketClient client;
    private readonly Queue<EventBase> events = new();
    private readonly Dictionary<string, (Type, ReceiptBase)> receipts = new();

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


    public async Task<EventBase> FetchAsync(CancellationToken token)
    {
        return await Task.Run(() =>
        {
            if (!client.IsStarted) client.Start().Wait(token);

            while (!token.IsCancellationRequested)
            {
                if (events.Count > 0) return events.Dequeue();

                Thread.Sleep(300);
            }

            return new ShutdownEvent();
        }, token);
    }

    public async Task<ReceiptBase> SendAsync(ActionBase action, CancellationToken token)
    {
        var echo = Guid.NewGuid().ToString();
        var json = ModelFactory.SerializeAction(action, echo);
        receipts.Add(echo, (action.Receipt, null));
        client.Send(json);

        return await Task.Run(() =>
        {
            while (!token.IsCancellationRequested)
            {
                if (receipts[echo].Item2 != null)
                {
                    var result = receipts[echo];
                    receipts.Remove(echo);
                    return result.Item2;
                }

                Thread.Sleep(300);
            }

            throw new OperationCanceledException("Timeout for waiting for some receipt");
        }, token);
    }

    ~WebsocketConnection()
    {
        if (client.IsRunning) client.Stop(WebSocketCloseStatus.NormalClosure, "Normal closure");
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
                var json = JsonConvert.DeserializeObject<JObject>(message.Text);
                if (json!.ContainsKey("echo"))
                {
                    // 回执
                    var echo = json.Value<string>("echo")!;
                    var receipt = ModelFactory.ConstructReceipt(json, receipts[echo].Item1);
                    receipts[echo] = (receipts[echo].Item1, receipt);
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
}