using System;
using System.Linq;
using System.Net;
using UniSpyServer.UniSpyLib.Abstraction.Interface;

namespace UniSpyServer.UniSpyLib.Abstraction.BaseClass.Network.Udp.Server
{
    /// <summary>
    /// This is a template class that helps creating a UDP Server with
    /// logging functionality and ServerName, as required in the old network stack.
    /// </summary>
    public abstract class UdpServer : NetCoreServer.UdpServer, IServer
    {
        public Guid ServerID { get; private set; }
        /// <summary>
        /// currently, we do not to care how to delete elements in dictionary
        /// </summary>
        public string ServerName { get; private set; }
        // The connections are handled by ClientBase
        // public ConcurrentDictionary<IPEndPoint, IConnection> Connections { get; private set; }
        IPEndPoint IServer.ListeningEndPoint => (IPEndPoint)Endpoint;
        public UdpServer(Guid serverID, string serverName, IPEndPoint endpoint) : base(endpoint)
        {
            ServerID = serverID;
            ServerName = serverName;
            // Connections = new ConcurrentDictionary<IPEndPoint, IConnection>();
        }
        public new void Start()
        {
            if (OptionSendBufferSize > int.MaxValue || OptionReceiveBufferSize > int.MaxValue)
            {
                throw new ArgumentException("Buffer size can not big than length of integer!");
            }
            base.Start();
        }

        protected override void OnStarted() => ReceiveAsync();

        /// <summary>
        /// Send unencrypted data
        /// </summary>
        /// <param name="buffer">plaintext</param>
        /// <returns>is sending succeed</returns>
        protected override void OnReceived(EndPoint endPoint, byte[] buffer, long offset, long size)
        {
            var connection = CreateConnection((IPEndPoint)endPoint);
            (connection as UdpConnection).OnReceived(buffer.Skip((int)offset).Take((int)size).ToArray());
        }
        protected abstract IClient CreateClient(IConnection connection);
        protected virtual IUdpConnection CreateConnection(IPEndPoint endPoint)
        {
            // we have to check if the endPoint is already in the dictionary,
            // which means the client is already in the dictionary, we do not need to create it
            // we just retrieve the connection from the dictionary
            // lock (Connections)
            // {
            // UdpConnection conn;
            if (ClientBase.ClientPool.TryGetValue(endPoint, out var client))
            {
                return client.Connection as IUdpConnection;
            }
            else
            {
                var connection = new UdpConnection(this, endPoint);
                client = CreateClient(connection);
                ClientBase.ClientPool.GetOrAdd(endPoint, client);
                return client.Connection as IUdpConnection;
                // return connection;
            }
            // }
        }
    }
}

