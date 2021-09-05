﻿using Chat.Abstraction.BaseClass;
using Chat.Entity.Exception;

namespace Chat.Entity.Structure.Request.General
{
    internal sealed class CDKEYRequest : ChatRequestBase
    {
        public CDKEYRequest(string rawRequest) : base(rawRequest)
        {
        }

        public string CDKey { get; private set; }

        public override void Parse()
        {
            base.Parse();
            if (_cmdParams.Count < 1)
                throw new ChatException("The number of IRC cmdParams are incorrect.");
            CDKey = _cmdParams[0];
        }
    }
}
