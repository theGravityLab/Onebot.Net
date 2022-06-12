using System;

namespace Onebot.Protocol
{
    public static class ConnectionFactory
    {
        public static IConnection FromWebsocket(string host, int port, string accessToken)
        {
            throw new NotImplementedException();
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
}