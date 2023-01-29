using System;
using System.Collections.Concurrent;
using System.Net;
using StackExchange.Redis;

namespace UniSpyServer.UniSpyLib.Abstraction.Interface
{
    public interface IServer
    {
        static IConnectionMultiplexer RedisConnection { get; }
        Guid ServerID { get; }
        string ServerName { get; }
        IPEndPoint ListeningEndPoint { get; }
        void Start();
    }
}
