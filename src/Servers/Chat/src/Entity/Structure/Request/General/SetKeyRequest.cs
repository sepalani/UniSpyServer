﻿using System.Collections.Generic;
using UniSpyServer.Servers.Chat.Abstraction.BaseClass;
using UniSpyServer.Servers.Chat.Entity.Contract;
using UniSpyServer.UniSpyLib.Extensions;

namespace UniSpyServer.Servers.Chat.Entity.Structure.Request.General
{
    [RequestContract("SETKEY")]
    public sealed class SetKeyRequest : RequestBase
    {
        public Dictionary<string, string> KeyValues { get; private set; }

        public SetKeyRequest(string rawRequest) : base(rawRequest){ }

        public override void Parse()
        {
            base.Parse();

            if (_longParam == null)
            {
                throw new Exception.ChatException("The keys and values are missing.");
            }
            KeyValues = StringExtensions.ConvertKVStringToDictionary(_longParam);
        }
    }
}
