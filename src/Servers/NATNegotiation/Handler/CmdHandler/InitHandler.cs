﻿using UniSpyLib.Abstraction.Interface;
using NATNegotiation.Abstraction.BaseClass;
using NATNegotiation.Entity.Structure.Request;
using NATNegotiation.Entity.Structure.Response;
using System;
using NATNegotiation.Entity.Structure;
using NATNegotiation.Entity.Structure.Result;

namespace NATNegotiation.Handler.CmdHandler
{
<<<<<<< HEAD
    internal class InitHandler : NNCommandHandlerBase
    {
        protected new InitRequest _request =>(InitRequest)base._request;
=======
    public class InitHandler : NNCmdHandlerBase
    {
        protected new InitRequest _request { get { return (InitRequest)base._request; } }
        protected new InitResult _result
        {
            get { return (InitResult)base._result; }
            set { base._result = value; }
        }
>>>>>>> c309f4b009e514a1d1f13db4317bdf0d8c2e4797
        protected NatUserInfo _userInfo;
        protected string _fullKey;
        public InitHandler(IUniSpySession session, IUniSpyRequest request) : base(session, request)
        {
        }

        protected override void RequestCheck()
        {
<<<<<<< HEAD
            //TODO we get user infomation from redis
=======
            _result = new InitResult();
>>>>>>> c309f4b009e514a1d1f13db4317bdf0d8c2e4797
            _fullKey = NatUserInfo.RedisOperator.BuildFullKey(
                _session.RemoteIPEndPoint,
                _request.PortType,
                _request.Cookie);
            _userInfo = NatUserInfo.RedisOperator.GetSpecificValue(_fullKey);

            if (_userInfo == null)
            {
                _userInfo = new NatUserInfo();
                _userInfo.UpdateRemoteEndPoint(_session.RemoteIPEndPoint);
            }

        }

        protected override void DataOperation()
        {            //TODO we get user infomation from redis

            _userInfo.UpdateInitRequestInfo(_request);
            _userInfo.LastPacketRecieveTime = DateTime.Now;
            NatUserInfo.RedisOperator.SetKeyValue(_fullKey, _userInfo);
        }

        protected override void ResponseConstruct()
        {
            _response = new InitResponse(_request, _result);
        }

        //protected override void Response()
        //{
        //    base.Response();
        //    NatNegotiateManager
        //        .Negotiate(
        //        _request.PortType,
        //        _request.Version,
        //        _request.Cookie);
        //}
    }
}
