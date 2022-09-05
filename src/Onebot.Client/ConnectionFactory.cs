using System;
using Onebot.Client.BuiltinConnections;

namespace Onebot.Client;

public static class ConnectionFactory
{
    public static IConnection FromWebsocket(string host, int port, string accessToken)
    {
        var socket = new WebsocketConnection(host, port, accessToken);
        return socket;
    }

    public static IConnection FromHttp(string host, int port, string accessToken)
    {
        throw new NotImplementedException();
    }

    public static IConnection FromUrl(Uri url)
    {
        throw new NotImplementedException();
    }
}