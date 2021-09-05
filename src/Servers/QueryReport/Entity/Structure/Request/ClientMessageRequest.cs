﻿using QueryReport.Abstraction.BaseClass;
using QueryReport.Entity.Enumerate;

namespace QueryReport.Entity.Structure.Request
{
    internal sealed class ClientMessageRequest : QRRequestBase
    {
        public new uint InstantKey
        {
            get => base.InstantKey;
            set => base.InstantKey = value;
        }
        public ClientMessageRequest(byte[] rawRequest) : base(rawRequest)
        {
        }

        public ClientMessageRequest()
        {
        }
    }
}
