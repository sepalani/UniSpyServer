﻿using CDKey.Network;
using UniSpyLib.Abstraction.BaseClass;
using UniSpyLib.Extensions;
using UniSpyLib.UniSpyConfig;
using System;
using System.Net;
namespace CDKey.Application
{
    /// <summary>
    /// A factory that create the instance of servers
    /// </summary>
    public class ServerManager : UniSpyServerManagerBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serverName">Server name in config file</param>
        public ServerManager(string serverName) : base(serverName)
        {
        }

        /// <summary>
        /// Starts a specific server
        /// </summary>
        /// <param name="cfg">The configuration of the specific server to run</param>
        protected override void StartServer(ServerConfig cfg)
        {
            if (cfg.Name == ServerName)
            {
                Server = new CDKeyServer(IPAddress.Parse(cfg.ListeningAddress), cfg.ListeningPort).Start();
                Console.WriteLine(
                    StringExtensions.FormatServerTableContext(cfg.Name, cfg.ListeningAddress, cfg.ListeningPort.ToString()));
            }
        }
    }
}
