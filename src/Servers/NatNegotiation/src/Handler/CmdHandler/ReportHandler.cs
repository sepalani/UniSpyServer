﻿using System;
using System.Linq;
using UniSpyServer.Servers.NatNegotiation.Abstraction.BaseClass;
using UniSpyServer.Servers.NatNegotiation.Entity.Contract;
using UniSpyServer.Servers.NatNegotiation.Entity.Enumerate;
using UniSpyServer.Servers.NatNegotiation.Entity.Exception;
using UniSpyServer.Servers.NatNegotiation.Entity.Structure.Redis;
using UniSpyServer.Servers.NatNegotiation.Entity.Structure.Request;
using UniSpyServer.Servers.NatNegotiation.Entity.Structure.Response;
using UniSpyServer.Servers.NatNegotiation.Entity.Structure.Result;
using UniSpyServer.UniSpyLib.Abstraction.BaseClass.Factory;
using UniSpyServer.UniSpyLib.Abstraction.Interface;

namespace UniSpyServer.Servers.NatNegotiation.Handler.CmdHandler
{
    /// <summary>
    /// Get nat neg result report success or fail
    /// </summary>
    [HandlerContract(RequestType.Report)]
    public sealed class ReportHandler : CmdHandlerBase
    {
        private new ReportRequest _request => (ReportRequest)base._request;
        private new ReportResult _result { get => (ReportResult)base._result; set => base._result = value; }
        private UserInfo _userInfo;
        public ReportHandler(IClient client, IRequest request) : base(client, request)
        {
            _result = new ReportResult();
        }

        protected override void DataOperation()
        {
            _userInfo = _redisClient.Values.Where(
            k => k.ServerID == ServerFactory.Server.ServerID
            & k.RemoteIPEndPoint == _client.Connection.RemoteIPEndPoint
            & k.PortType == _request.PortType
            & k.Cookie == _request.Cookie).FirstOrDefault();

            if (_userInfo == null)
            {
                throw new NNException("No user found in redis.");
            }

            if (_request.NatResult != NatNegResult.Success)
            {
                foreach (NatPortType portType in Enum.GetValues(typeof(NatPortType)))
                {
                    var request = new ConnectRequest
                    {
                        PortType = portType,
                        Version = _request.Version,
                        Cookie = _request.Cookie
                    };
                    new ConnectHandler(_client, request).Handle();
                }

                _userInfo.RetryNATNegotiationTime++;
                _redisClient.SetValue(_userInfo);
            }
            else
            {
                // natnegotiation successed we delete the negotiator
                _redisClient.DeleteKeyValue(_userInfo);
            }
        }

        protected override void ResponseConstruct()
        {
            _response = new ReportResponse(_request, _result);
        }
    }
}
