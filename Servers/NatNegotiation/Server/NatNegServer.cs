﻿using GameSpyLib.Network;
using NatNegotiation.Entity.Structure;
using NatNegotiation.Handler.CommandHandler.CommandSwitcher;
using System.Collections.Concurrent;
using System.Net;

namespace NatNegotiation.Server
{
    public class NatNegServer : TemplateUdpServer
    {
        public static ConcurrentDictionary<EndPoint, NatNegClientInfo> ClientInfoList;

        protected readonly ConcurrentDictionary<EndPoint, NatNegSession> Clients;

        public NatNegServer(IPAddress address, int port) : base(address, port)
        {
            ClientInfoList = new ConcurrentDictionary<EndPoint, NatNegClientInfo>();
            Clients = new ConcurrentDictionary<EndPoint, NatNegSession>();
        }

        public override bool Start()
        {
            //new ClientListManager().Start();
            return base.Start();
        }


        protected override object CreateClient(EndPoint endPoint)
        {
            return new NatNegSession(this, endPoint);
        }

        protected override void OnReceived(EndPoint endPoint, byte[] message)
        {
            NatNegSession client;
            if (!Clients.TryGetValue(endPoint, out client))
            {
                client = (NatNegSession)CreateClient(endPoint);
                Clients.TryAdd(endPoint, client);
            }

            new NatNegCommandSwitcher().Switch(client, message);
        }
    }
}
