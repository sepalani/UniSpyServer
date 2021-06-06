﻿using Chat.Abstraction.BaseClass;
using Chat.Entity.Exception;

namespace Chat.Entity.Structure.Request.General
{
    public class CRYPTRequest : ChatRequestBase
    {
        public CRYPTRequest(string rawRequest) : base(rawRequest)
        {
        }

        public string VersionID { get; protected set; }
        public string GameName { get; protected set; }
        //CRYPT des %d %s

        public override void Parse()
        {
            base.Parse();

            if (_cmdParams.Count < 3)
                throw new ChatException("The number of IRC params in CRYPT request is incorrect.");

            VersionID = _cmdParams[1];
            GameName = _cmdParams[2];
        }
    }
}
