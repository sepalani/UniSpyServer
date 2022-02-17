﻿using System.Linq;
using UniSpyServer.Servers.Chat.Abstraction.BaseClass;
using UniSpyServer.Servers.Chat.Application;
using UniSpyServer.Servers.Chat.Entity.Contract;
using UniSpyServer.Servers.Chat.Entity.Exception.IRC.General;
using UniSpyServer.Servers.Chat.Entity.Structure.Misc;
using UniSpyServer.Servers.Chat.Entity.Structure.Request.General;
using UniSpyServer.Servers.Chat.Entity.Structure.Response.General;
using UniSpyServer.Servers.Chat.Entity.Structure.Result.General;
using UniSpyServer.Servers.Chat.Network;
using UniSpyServer.UniSpyLib.Abstraction.Interface;

namespace UniSpyServer.Servers.Chat.Handler.CmdHandler.General
{
    [HandlerContract("WHOIS")]
    public sealed class WhoIsHandler : CmdHandlerBase
    {
        private new WhoIsRequest _request => (WhoIsRequest)base._request;
        private new WhoIsResult _result { get => (WhoIsResult)base._result; set => base._result = value; }
        private UserInfo _userInfo;
        public WhoIsHandler(IClient client, IRequest request) : base(client, request)
        {
        }

        protected override void RequestCheck()
        {
            _result = new WhoIsResult();

            // there only existed one nick name
            base.RequestCheck();
            var session = (Session)ServerFactory.Server.SessionManager.SessionPool.Values
                 .Where(s => ((Session)s).UserInfo.NickName == _request.NickName)
                 .FirstOrDefault();
            if (session == null)
            {
                throw new ChatIRCNoSuchNickException($"Can not find user with nickname:{_request.NickName}.");
            }
            _userInfo = session.UserInfo;
        }
        protected override void DataOperation()
        {
            _result.NickName = _userInfo.NickName;
            _result.Name = _userInfo.Name;
            _result.UserName = _userInfo.UserName;
            _result.PublicIPAddress = _userInfo.RemoteIPEndPoint.Address.ToString();
            foreach (var channel in _userInfo.JoinedChannels.Values)
            {
                _result.JoinedChannelName.Add(channel.Name);
            }
        }
        protected override void ResponseConstruct()
        {
            _response = new WhoIsResponse(_request, _result);
        }


    }
}
