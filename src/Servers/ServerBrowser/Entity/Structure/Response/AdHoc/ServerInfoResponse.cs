using ServerBrowser.Abstraction.BaseClass;
using ServerBrowser.Entity.Enumerate;
using ServerBrowser.Entity.Structure.Misc;
using ServerBrowser.Entity.Structure.Result;
using System.Text;
using UniSpyLib.Abstraction.BaseClass;
using UniSpyLib.Encryption;
using UniSpyLib.Extensions;

namespace ServerBrowser.Entity.Structure.Response
{
    /// <summary>
    /// Get a server's information
    /// </summary>
    internal sealed class ServerInfoResponse : ServerListUpdateOptionResponseBase
    {
        private new ServerInfoResult _result => (ServerInfoResult)base._result;
        public ServerInfoResponse(UniSpyRequestBase request, UniSpyResultBase result) : base(request, result)
        {
        }

        protected override void BuildNormalResponse()
        {
            _serverListContext.Add((byte)SBServerResponseType.PushServerMessage);
            BuildServersInfo();
            // add message length here
            _serverListContext.InsertRange(0, ByteTools.GetBytes((short)(_serverListContext.Count + 2), true));
        }

        protected override void BuildServersInfo()
        {
            BuildServerInfoHeader(
                GameServerFlags.HasFullRulesFlag,
                _result.GameServerInfo);

            foreach (var kv in _result.GameServerInfo.ServerData.KeyValue)
            {
                _serverListContext.AddRange(UniSpyEncoding.GetBytes(kv.Key));
                _serverListContext.Add(SBStringFlag.StringSpliter);
                _serverListContext.AddRange(UniSpyEncoding.GetBytes(kv.Value));
                _serverListContext.Add(SBStringFlag.StringSpliter);
            }
            foreach (var player in _result.GameServerInfo.PlayerData.KeyValueList)
            {
                foreach (var kv in player)
                {
                    _serverListContext.AddRange(UniSpyEncoding.GetBytes(kv.Key));
                    _serverListContext.Add(SBStringFlag.StringSpliter);
                    _serverListContext.AddRange(UniSpyEncoding.GetBytes(kv.Value));
                    _serverListContext.Add(SBStringFlag.StringSpliter);
                }
            }
            foreach (var team in _result.GameServerInfo.TeamData.KeyValueList)
            {
                foreach (var kv in team)
                {
                    _serverListContext.AddRange(UniSpyEncoding.GetBytes(kv.Key));
                    _serverListContext.Add(SBStringFlag.StringSpliter);
                    _serverListContext.AddRange(UniSpyEncoding.GetBytes(kv.Value));
                    _serverListContext.Add(SBStringFlag.StringSpliter);
                }
            }
        }
    }
}