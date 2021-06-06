﻿using Chat.Abstraction.BaseClass;

namespace Chat.Entity.Structure.Request.General
{
    public class LOGINPREAUTHRequest : ChatRequestBase
    {
        public LOGINPREAUTHRequest(string rawRequest) : base(rawRequest)
        {
        }

        public string AuthToken { get; protected set; }
        public string PartnerChallenge { get; protected set; }

        public override void Parse()
        {
            base.Parse();


            AuthToken = _cmdParams[0];
            PartnerChallenge = _cmdParams[1];
        }
    }
}
