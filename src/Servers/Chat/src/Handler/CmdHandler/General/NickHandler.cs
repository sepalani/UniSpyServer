using System.Linq;
using UniSpy.Server.Chat.Abstraction.BaseClass;
using UniSpy.Server.Chat.Application;
using UniSpy.Server.Chat.Exception.IRC.General;
using UniSpy.Server.Chat.Contract.Request.General;
using UniSpy.Server.Chat.Contract.Response.General;
using UniSpy.Server.Core.Abstraction.Interface;
using UniSpy.Server.Chat.Aggregate.Redis;

namespace UniSpy.Server.Chat.Handler.CmdHandler.General
{

    public sealed class NickHandler : CmdHandlerBase
    {
        private new NickRequest _request => (NickRequest)base._request;
        public NickHandler(IClient client, IRequest request) : base(client, request) { }

        protected override void RequestCheck()
        {
            base.RequestCheck();
            int number = 0;
            string validNickName;
            if (Client.ClientPool.Values.Where(x => ((ClientInfo)x.Info).NickName == _request.NickName).Count() == 1
            || GeneralMessageChannel.RemoteClients.Values.Where(x => ((ClientInfo)x.Info).NickName == _request.NickName).Count() == 1)
            {
                while (true)
                {
                    string newNickName = _request.NickName + number;
                    if (Client.ClientPool.Values.Where(x => ((ClientInfo)x.Info).NickName == newNickName).Count() == 0
                        && GeneralMessageChannel.RemoteClients.Values.Where(x => ((ClientInfo)x.Info).NickName == newNickName).Count() == 0)
                    {
                        validNickName = newNickName;
                        break;
                    }
                }
                throw new ChatIRCNickNameInUseException(
                    $"The nick name: {_request.NickName} is already in use",
                    _request.NickName,
                    validNickName);
            }
            _client.Info.NickName = _request.NickName;
        }

        protected override void DataOperation()
        {
            _client.Info.NickName = _request.NickName;
        }
        protected override void ResponseConstruct()
        {
            _response = new NickResponse(_request, _result);
        }
    }
}
