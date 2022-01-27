﻿using System;
using System.Linq;
using UniSpyServer.Servers.QueryReport.Abstraction.BaseClass;
using UniSpyServer.Servers.QueryReport.Entity.contract;
using UniSpyServer.Servers.QueryReport.Entity.Enumerate;
using UniSpyServer.Servers.QueryReport.Entity.Structure.Request;
using UniSpyServer.Servers.QueryReport.Entity.Structure.Result;
using UniSpyServer.UniSpyLib.Abstraction.Interface;
using UniSpyServer.UniSpyLib.Logging;

namespace UniSpyServer.Servers.QueryReport.Handler.CmdHandler
{
    [HandlerContract(RequestType.Echo)]
    public sealed class EchoHandler : CmdHandlerBase
    {
        private new EchoRequest _request => (EchoRequest)base._request;
        private new EchoResult _result { get => (EchoResult)base._result; set => base._result = value; }
        public EchoHandler(IUniSpySession session, IUniSpyRequest request) : base(session, request)
        {
            _result = new EchoResult();
        }

        protected override void DataOperation()
        {
            //TODO prevent one pc create multiple game servers
            var servers = _redisClient.Values.Where(x => x.InstantKey == _request.InstantKey).ToList();
            if (servers.Count() != 1)
            {
                LogWriter.Info("Can not find game server");
                return;
            }
            //add recive echo packet on gameserverList

            //we get the first key in matchedKeys
            _result.Info = servers.First();
            _result.Info.LastPacketReceivedTime = DateTime.Now;
            _redisClient.SetValue(_result.Info);
        }
    }
}
